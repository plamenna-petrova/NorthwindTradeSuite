using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.DTOs.Requests.Categories
{
    public class CreateCategoryRequestDTO
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public byte[] Picture { get; set; } = null!;

        public string CreatedBy { get; set; } = null!;
    }
}
