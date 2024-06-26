using AutoMapper;

namespace NorthwindTradeSuite.Services.Mapper.Contracts
{
    public interface ICustomMap
    {
        void CreateMap(IProfileExpression profileExpression);
    }
}
