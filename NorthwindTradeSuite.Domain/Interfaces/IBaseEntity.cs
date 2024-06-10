using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Domain.Interfaces
{
    public interface IBaseEntity<TKey>
    {
        [Key]
        TKey ID { get; set; }
    }
}
