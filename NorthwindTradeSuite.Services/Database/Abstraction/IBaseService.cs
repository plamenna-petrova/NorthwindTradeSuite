using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Services.Database.Abstraction
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<TDTO> FindByIdAsync<TDTO>(string id);

        Task<TDTO> GetByIdAsync<TDTO>(string id, bool isDeletedFlag);

        Task<TDTO> CreateAsync<TDTO, TCreateDTO>(TCreateDTO createDTO, string currentUserId);

        Task<TDTO> UpdateAsync<TDTO, TUpdateDTO>(string id, TUpdateDTO updateDTO, string currentUserId);

        Task<TDTO> HardDeleteAsync<TDTO>(string id);

        Task<TDTO> DeleteAsync<TDTO>(string id, string currentUserId);

        Task<TDTO> RestoreAsync<TDTO>(string id, string currentUserId);

        IQueryable<TEntity> GetAll();

        Task<List<TDTO>> GetAllAsync<TDTO>();

        Task<List<TDTO>> GetAllAsync<TDTO>(bool isDeletedFlag);
    }
}
