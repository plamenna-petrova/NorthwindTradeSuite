using NorthwindTradeSuite.Domain.Abstraction;

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

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Title { get; set; }

        public string TitleOfCourtesy { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string HomePhone { get; set; }

        public string Extension { get; set; }

        public byte[] Photo { get; set; }

        public string Notes { get; set; }

        public string ReportsTo { get; set; }

        public virtual Employee Manager { get; set; }

        public string PhotoPath { get; set; }

        public virtual ICollection<EmployeeTerritory> EmployeeTerritories { get; set; }

        public virtual ICollection<Employee> DirectReports { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
