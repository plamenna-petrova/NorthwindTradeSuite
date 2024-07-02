using AutoMapper;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Domain.Entities.OwnedEntities;
using NorthwindTradeSuite.DTOs.Seeding;

namespace NorthwindTradeSuite.Mapping.AutoMapper.Profiles
{
    internal class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<SeedOrderDTO, Order>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.RequiredDate, opt => opt.MapFrom(src => src.RequiredDate))
                .ForMember(dest => dest.ShippedDate, opt => opt.MapFrom(src => src.ShippedDate))
                .ForMember(dest => dest.ShipperId, opt => opt.MapFrom(src => src.ShipperId))
                .ForMember(dest => dest.Freight, opt => opt.MapFrom(src => src.Freight))
                .ForMember(dest => dest.ShipName, opt => opt.MapFrom(src => src.ShipName))
                .ForMember(dest => dest.LocationData, opt => opt.MapFrom(src => new LocationData
                {
                    Address = src.ShipAddress,
                    City = src.ShipCity,
                    Region = src.ShipRegion,
                    PostalCode = src.ShipPostalCode,
                    Country = src.ShipCountry
                }))
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.Shipper, opt => opt.Ignore());
        }
    }
}
