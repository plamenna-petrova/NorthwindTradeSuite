using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Services.Database.Abstraction
{
    public interface IBaseService<TEntity> where TEntity : class
    {    
        IQueryable<TEntity> GetAll(bool asNoTracking = false);

        Task<List<TDTO>> GetAllAsync<TDTO>(bool asNoTracking = false);

        Task<TDTO> GetByIdAsync<TDTO>(string id);

        Task<TDTO> CreateAsync<TDTO, TCreateDTO>(TCreateDTO createDTO, string currentUserId);

        Task<TDTO> UpdateAsync<TDTO, TUpdateDTO>(string id, TUpdateDTO updateDTO, string currentUserId);

        Task<TDTO> HardDeleteAsync<TDTO>(string id);

        Task<TDTO> DeleteAsync<TDTO>(string id, string currentUserId);

        Task<TDTO> RestoreAsync<TDTO>(string id, string currentUserId);
    }
}
