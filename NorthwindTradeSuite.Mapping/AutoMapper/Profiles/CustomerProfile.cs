using AutoMapper;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Domain.Entities.OwnedEntities;
using NorthwindTradeSuite.DTOs.Seeding;
using NorthwindTradeSuite.DTOs.Seeding.SharedSeedingDTOs;

namespace NorthwindTradeSuite.Mapping.AutoMapper.Profiles
{
    internal class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<SeedProfessionalDataDTO, ProfessionalData>();
            CreateMap<SeedLocationDataDTO, LocationData>();
            CreateMap<SeedPersonalContactDataDTO, PersonalContactData>();

            CreateMap<SeedCustomerDTO, Customer>()
                .ForMember(dest => dest.ProfessionalData, opt => opt.MapFrom(src => src.SeedProfessionalDataDTO))
                .ForMember(dest => dest.LocationData, opt => opt.MapFrom(src => src.SeedLocationDataDTO))
                .ForMember(dest => dest.PersonalContactData, opt => opt.MapFrom(src => src.SeedPersonalContactDataDTO))
                .ForMember(dest => dest.Orders, opt => opt.Ignore());
        }
    }
}
