using CsvHelper.Configuration.Attributes;
using NorthwindTradeSuite.DTOs.Seeding.SharedSeedingDTOs;
using NorthwindTradeSuite.DTOs.Seeding.TypeConverters;

namespace NorthwindTradeSuite.DTOs.Seeding
{
    public class SeedEmployeeDTO
    {
        [Name("employeeID")]
        public string Id { get; set; } = null!;

        [Name("lastName")]
        public string LastName { get; set; } = null!;

        [Name("firstName")]
        public string FirstName { get; set; } = null!;

        [Name("title")]
        public string Title { get; set; } = null!;

        [Name("titleOfCourtesy")]
        public string TitleOfCourtesy { get; set; } = null!;

        [Name("birthDate")]
        [TypeConverter(typeof(NullableDateTimeConverter))]
        public DateTime? BirthDate { get; set; }

        [Name("hireDate")]
        [TypeConverter(typeof(NullableDateTimeConverter))]
        public DateTime? HireDate { get; set; }

        public SeedLocationDataDTO SeedLocationDTO { get; set; } = null!;

        [Name("homePhone")]
        public string HomePhone { get; set; } = null!;

        [Name("extension")]
        public string Extension { get; set; } = null!;

        [Name("photo")]
        public byte[] Photo { get; set; } = null!;

        [Name("notes")]
        public string Notes { get; set; } = null!;

        [Name("reportsTo")]
        public string ReportsTo { get; set; } = null!;

        [Name("photoPath")]
        public string PhotoPath { get; set; } = null!;
    }
}
