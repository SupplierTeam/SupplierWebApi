using SupplierWebApi.IRepositories.Base;
using SupplierWebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplierWebApi.IRepositories
{
    public interface IUserRepository1 : IBaseRepository1<User>
    {
        User GetUserById(string id);

        bool AddUser(User entity);

        bool UpdateUser(User entity);

        bool DeleteUser(User entity);

        IList<User> GetUserAll();
    }
}
