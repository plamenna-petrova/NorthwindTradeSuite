using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Services.Database.Abstraction
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        public Task<TDTO> CreateAsync<TDTO, TCreateDTO>(TCreateDTO createDTO, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<TDTO> DeleteAsync<TDTO>(string id, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<TDTO> FindByIdAsync<TDTO>(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<TDTO>> GetAllAsync<TDTO>()
        {
            throw new NotImplementedException();
        }

        public Task<List<TDTO>> GetAllAsync<TDTO>(bool isDeletedFlag)
        {
            throw new NotImplementedException();
        }

        public Task<TDTO> GetByIdAsync<TDTO>(string id, bool isDeletedFlag)
        {
            throw new NotImplementedException();
        }

        public Task<TDTO> HardDeleteAsync<TDTO>(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TDTO> RestoreAsync<TDTO>(string id, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<TDTO> UpdateAsync<TDTO, TUpdateDTO>(string id, TUpdateDTO updateDTO, string currentUserId)
        {
            throw new NotImplementedException();
        }
    }
}
