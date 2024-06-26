using AutoMapper;
using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.Services.Mapper.Contracts;
using System.Reflection;

namespace NorthwindTradeSuite.Services.Mapper
{
    public static class AutoMapperConfigurator
    {
        private static bool isInitialized;

        public static IMapper MapperInstance { get; set; } = null!;

        public static void RegisterMappings(params Assembly[] assemblies)
        {
            if (isInitialized)
            {
                return;
            }

            isInitialized = true;

            List<Type> exportedTypesFromAssemblies = assemblies
                .SelectMany(a => a.GetExportedTypes())
                .ToList();

            MapperConfigurationExpression mapperConfigurationExpression = new();

            mapperConfigurationExpression.CreateProfile(
                "ReflectionProfile",
                profileExpression =>
                {
                    foreach (var map in GetFromMaps(exportedTypesFromAssemblies))
                    {
                        mapperConfigurationExpression.CreateMap(map.SourceType, map.DestinationType);
                    }

                    foreach (var map in GetToMaps(exportedTypesFromAssemblies))
                    {
                        mapperConfigurationExpression.CreateMap(map.SourceType, map.DestinationType);
                    }

                    foreach (var map in GetCustomMaps(exportedTypesFromAssemblies))
                    {
                        map.CreateMap(mapperConfigurationExpression);
                    }

                    foreach (var map in GetBaseEntityTypes(exportedTypesFromAssemblies))
                    {
                        mapperConfigurationExpression.CreateMap(map.SourceType, map.DestinationType);
                    }

                    foreach (var map in GetEntityTypesToBaseEntity(exportedTypesFromAssemblies))
                    {
                        mapperConfigurationExpression.CreateMap(map.SourceType, map.DestinationType);
                    }

                    foreach (var map in GetBaseDeletableEntityTypes(exportedTypesFromAssemblies))
                    {
                        mapperConfigurationExpression.CreateMap(map.SourceType, map.DestinationType);
                    }

                    foreach (var map in GetEntityTypesToBaseDeletableEntity(exportedTypesFromAssemblies))
                    {
                        mapperConfigurationExpression.CreateMap(map.SourceType, map.DestinationType);
                    }
                }
            );

            // add profiles

            MapperInstance = new AutoMapper.Mapper(new MapperConfiguration(mapperConfigurationExpression));
        }

        private static IEnumerable<TypesMap> GetBaseEntityTypes(IEnumerable<Type> types)
        {
            IEnumerable<TypesMap> baseEntityTypes = GetTypesWithBaseEntityType(types)
                .Select(t => new TypesMap { SourceType = t, DestinationType = t });

            return baseEntityTypes;
        }

        private static IEnumerable<TypesMap> GetEntityTypesToBaseEntity(IEnumerable<Type> types)
        {
            IEnumerable<TypesMap> entityTypesToBaseEntity = GetTypesWithBaseEntityType(types)
                .Select(t => new TypesMap { SourceType = t, DestinationType = typeof(BaseEntity<string>) });

            return entityTypesToBaseEntity;
        }

        private static IEnumerable<TypesMap> GetBaseDeletableEntityTypes(IEnumerable<Type> types)
        {
            IEnumerable<TypesMap> baseDeletableEntityTypes = GetTypesWithBaseDeletableEntityType(types)
                .Select(t => new TypesMap { SourceType = t, DestinationType = t });

            return baseDeletableEntityTypes;
        }

        private static IEnumerable<TypesMap> GetEntityTypesToBaseDeletableEntity(IEnumerable<Type> types)
        {
            IEnumerable<TypesMap> entityTypesToBaseDeletableEntity = GetTypesWithBaseDeletableEntityType(types)
                .Select(t => new TypesMap { SourceType = t, DestinationType = t });

            return entityTypesToBaseDeletableEntity;
        }

        private static IEnumerable<Type> GetTypesWithBaseEntityType(IEnumerable<Type> types)
        {
            IEnumerable<Type> baseEntityTypes = types
                .Where(t => t.GetTypeInfo().BaseType == typeof(BaseEntity<string>) && 
                            !t.GetTypeInfo().IsAbstract && 
                            !t.GetTypeInfo().IsInterface);

            return baseEntityTypes;
        }

        private static IEnumerable<Type> GetTypesWithBaseDeletableEntityType(IEnumerable<Type> types)
        {
            IEnumerable<Type> baseDeletableEntityTypes = types
                .Where(t => t.GetTypeInfo().BaseType == typeof(BaseDeletableEntity<string>) &&
                            !t.GetTypeInfo().IsAbstract &&
                            !t.GetTypeInfo().IsInterface);

            return baseDeletableEntityTypes;
        }

        private static IEnumerable<TypesMap> GetFromMaps(IEnumerable<Type> types)
        {
            IEnumerable<TypesMap> fromMaps = types
                .SelectMany(t => t.GetTypeInfo().GetInterfaces(), (t, i) => new { Type = t, Interface = i })
                .Where(x => x.Interface.GetTypeInfo().IsGenericType)
                .Where(x => x.Interface.GetTypeInfo().GetGenericTypeDefinition() == typeof(IMapFrom<>))
                .Where(x => !x.Type.GetTypeInfo().IsAbstract)
                .Where(x => !x.Type.GetTypeInfo().IsInterface)
                .Select(x => new TypesMap
                {
                    SourceType = x.Interface.GetTypeInfo().GetGenericArguments()[0],
                    DestinationType = x.Type
                });

            return fromMaps;
        }

        private static IEnumerable<TypesMap> GetToMaps(IEnumerable<Type> types)
        {
            IEnumerable<TypesMap> toMaps = types
                .SelectMany(t => t.GetTypeInfo().GetInterfaces(), (t, i) => new { Type = t, Interface = i })
                .Where(x => x.Interface.GetTypeInfo().IsGenericType)
                .Where(x => x.Interface.GetTypeInfo().GetGenericTypeDefinition() == typeof(IMapTo<>))
                .Where(x => !x.Type.GetTypeInfo().IsAbstract)
                .Where(x => !x.Type.GetTypeInfo().IsInterface)
                .Select(x => new TypesMap
                {
                    SourceType = x.Type,
                    DestinationType = x.Interface.GetTypeInfo().GetGenericArguments()[0]
                });

            return toMaps;
        }

        private static IEnumerable<ICustomMap> GetCustomMaps(IEnumerable<Type> types)
        {
            IEnumerable<ICustomMap> customMaps = types
                .SelectMany(t => t.GetTypeInfo().GetInterfaces(), (t, i) => new { Type = t, Interface = i })
                .Where(x => typeof(ICustomMap).GetTypeInfo().IsAssignableFrom(x.Type))
                .Where(x => !x.Type.GetTypeInfo().IsAbstract)
                .Where(x => !x.Type.GetTypeInfo().IsInterface)
                .Select(x => (ICustomMap)Activator.CreateInstance(x.Type)!)!;

            return customMaps;
        }
    }
}
