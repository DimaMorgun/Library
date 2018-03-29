using System.Collections.Generic;

namespace Library.DataAccessLayer.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        int Insert(TEntity item);
        void Insert(List<TEntity> items);
        TEntity Get(int id);
        List<TEntity> GetAll();
        void Update(TEntity item);
        void Update(List<TEntity> items);
        void Delete(TEntity item);
        void Delete(List<TEntity> items);
    }
}
