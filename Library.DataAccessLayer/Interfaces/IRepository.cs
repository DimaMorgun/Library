using System.Collections.Generic;

namespace Library.DataAccessLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetList();
        T GetByid(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
