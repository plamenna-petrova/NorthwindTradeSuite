using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.Domain.Entities.OwnedEntities;

namespace NorthwindTradeSuite.Domain.Entities
{
    public class Employee : BaseEntity<string>
    {
        public Employee()
        {
            EmployeeTerritories = new HashSet<EmployeeTerritory>();
            DirectReports = new HashSet<Employee>();
            Orders = new HashSet<Order>();
        }

        public string LastName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string TitleOfCourtesy { get; set; } = null!;

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

        public LocationData LocationData { get; set; } = new();

        public string HomePhone { get; set; } = null!;

        public string Extension { get; set; } = null!;

        public byte[] Photo { get; set; } = null!;

        public string Notes { get; set; } = null!;

        public string ReportsTo { get; set; } = null!;

        public virtual Employee Manager { get; set; } = null!;

        public string PhotoPath { get; set; } = null!;

        public virtual ICollection<EmployeeTerritory> EmployeeTerritories { get; set; }

        public virtual ICollection<Employee> DirectReports { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
