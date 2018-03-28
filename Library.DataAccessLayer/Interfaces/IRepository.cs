using System.Collections.Generic;

namespace Library.DataAccessLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Insert(T item);
        void Insert(List<T> items);
        T Get(int id);
        List<T> GetAll();
        void Update(T item);
        void Update(List<T> items);
        void Delete(T item);
        void Delete(List<T> items);
    }
}
