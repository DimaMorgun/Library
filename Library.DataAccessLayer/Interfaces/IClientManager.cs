using Library.EntityLayer.Identity;

using System;

namespace Library.DataAccessLayer.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}
