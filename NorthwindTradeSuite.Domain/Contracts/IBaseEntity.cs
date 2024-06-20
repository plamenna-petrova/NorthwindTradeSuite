using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Domain.Contracts
{
    public interface IBaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        [Key]
        TKey Id { get; set; }
    }
}
