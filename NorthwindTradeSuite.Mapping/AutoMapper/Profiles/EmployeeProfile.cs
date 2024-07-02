using AutoMapper;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Domain.Entities.OwnedEntities;
using NorthwindTradeSuite.DTOs.Seeding;
using NorthwindTradeSuite.DTOs.Seeding.SharedSeedingDTOs;

namespace NorthwindTradeSuite.Mapping.AutoMapper.Profiles
{
    internal class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<SeedLocationDataDTO, LocationData>();

            CreateMap<SeedEmployeeDTO, Employee>()
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.TitleOfCourtesy, opt => opt.MapFrom(src => src.TitleOfCourtesy))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.HireDate))
                .ForMember(dest => dest.LocationData, opt => opt.MapFrom(src => src.SeedLocationDTO))
                .ForMember(dest => dest.HomePhone, opt => opt.MapFrom(src => src.HomePhone))
                .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => src.Extension))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Photo))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.ReportsTo, opt => opt.Ignore())
                .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.PhotoPath))
                .ForMember(dest => dest.Manager, opt => opt.Ignore())
                .ForMember(dest => dest.EmployeeTerritories, opt => opt.Ignore())
                .ForMember(dest => dest.DirectReports, opt => opt.Ignore())
                .ForMember(dest => dest.Orders, opt => opt.Ignore());
        }
    }
}
