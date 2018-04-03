using Library.DataAccessLayer.Connection;

using Dapper.Contrib.Extensions;

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Library.DataAccessLayer.Repositories
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        private SqlConnection _connection;

        public GenericRepository()
        {
            _connection = new SqlConnection(CurrentConnection.ConnectionString);
        }
        public int Insert(TEntity item)
        {
            var id = (int)_connection.Insert(item);
            return id;
        }
        public void Insert(List<TEntity> items)
        {
            _connection.Insert(items);
        }
        public TEntity Get(int id)
        {
            var item = _connection.Get<TEntity>(id);
            return item;
        }
        public List<TEntity> GetAll()
        {
            var items = _connection.GetAll<TEntity>();
            return items.ToList();
        }
        public void Update(TEntity item)
        {
            _connection.Update(item);
        }
        public void Update(List<TEntity> items)
        {
            _connection.Update(items);
        }
        public void Delete(TEntity item)
        {
            _connection.Delete(item);
        }

        public void Delete(List<TEntity> items)
        {
            _connection.Delete(items);
        }
    }
}
