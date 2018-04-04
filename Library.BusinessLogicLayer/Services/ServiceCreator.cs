using Library.BusinessLogicLayer.Interfaces;
using Library.DataAccessLayer.UnitOfWork;

namespace Library.BusinessLogicLayer.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new UnitOfWork(connection));
        }
    }
}
