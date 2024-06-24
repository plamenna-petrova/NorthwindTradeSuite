using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace NorthwindTradeSuite.DTOs.Seeding.TypeConverters
{
    public class NullableDateTimeConverter : DefaultTypeConverter
    {
        public override object? ConvertFromString(string text, IReaderRow readerRow, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text) || text.Trim().Equals("NULL", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            if (DateTime.TryParse(text, out var date))
            {
                return date;
            }

            return base.ConvertFromString(text, readerRow, memberMapData);
        }
    }
}
