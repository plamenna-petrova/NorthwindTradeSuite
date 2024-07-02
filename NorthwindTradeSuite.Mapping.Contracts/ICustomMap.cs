using AutoMapper;

namespace NorthwindTradeSuite.Mapping.Contracts
{
    public interface ICustomMap
    {
        void CreateMap(IProfileExpression profileExpression);
    }
}
