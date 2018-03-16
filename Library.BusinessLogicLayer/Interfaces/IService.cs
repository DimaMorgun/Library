using System.Collections.Generic;

namespace Library.BusinessLogicLayer.Interfaces
{
    interface IService<T> where T : class
    {
        void Create(T book, int[] selectedItems);

        void Delete(int id);

        T GetByid(int id);

        List<T> GetList();

        void Update(T book, int[] selectedItems);
    }
}
