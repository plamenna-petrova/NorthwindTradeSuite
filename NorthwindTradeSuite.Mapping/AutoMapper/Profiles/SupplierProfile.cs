using AutoMapper;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Domain.Entities.OwnedEntities;
using NorthwindTradeSuite.DTOs.Seeding;
using NorthwindTradeSuite.DTOs.Seeding.SharedSeedingDTOs;

namespace NorthwindTradeSuite.Mapping.AutoMapper.Profiles
{
    internal class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<SeedProfessionalDataDTO, ProfessionalData>();
            CreateMap<SeedLocationDataDTO, LocationData>();
            CreateMap<SeedPersonalContactDataDTO, PersonalContactData>();

            CreateMap<SeedSupplierDTO, Supplier>()
                .ForMember(dest => dest.ProfessionalData, opt => opt.MapFrom(src => src.SeedProfessionalDataDTO))
                .ForMember(dest => dest.LocationData, opt => opt.MapFrom(src => src.SeedLocationDataDTO))
                .ForMember(dest => dest.PersonalContactData, opt => opt.MapFrom(src => src.SeedPersonalContactDataDTO))
                .ForMember(dest => dest.HomePage, opt => opt.MapFrom(src => src.HomePage))
                .ForMember(dest => dest.Products, opt => opt.Ignore());
        }
    }
}
