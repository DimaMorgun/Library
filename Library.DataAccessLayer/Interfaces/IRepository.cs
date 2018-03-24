using System.Collections.Generic;

namespace Library.DataAccessLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T Get(int id);
        void Insert(T item);
        void Insert(List<T> item);
        void Update(T item);
        void Update(List<T> items);
        void Delete(T item);
    }
}
