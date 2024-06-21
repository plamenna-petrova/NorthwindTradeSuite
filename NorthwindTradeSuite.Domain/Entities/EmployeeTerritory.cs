namespace NorthwindTradeSuite.Domain.Entities
{
    public class EmployeeTerritory
    {
        public string EmployeeId { get; set; } = null!;

        public virtual Employee Employee { get; set; } = null!;

        public string TerritoryId { get; set; } = null!;

        public virtual Territory Territory { get; set; } = null!;
    }
}