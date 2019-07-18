using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QQmusic.Core.Entities;

namespace QQmusic.Core.Interfaces
{
    public interface IRepository<TEntity, in TParameters> where TEntity : Entity where TParameters : QueryParameters
    {
        Task<PaginatedList<TEntity>> GetAllAsync(TParameters parameters);
        Task<TEntity> GetBySnAsync(decimal sn);
        void Add(TEntity t);
        void Delete(TEntity t);
        void Update(TEntity t);
    }

    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(decimal sn);
        void Add(TEntity t);
        void Delete(TEntity t);
        void Update(TEntity t);
    }
}
