using SupplierWebApi.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SupplierWebApi.Models.Domain;

namespace SupplierWebApi.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetUserById(string id);

        bool AddUser(User entity);

        bool AddUserList(List<User> entityList);

        bool UpdateUser(User entity);

        bool UpdateUserList(List<User> entityList);

        bool DeleteUser(string id);

        IList<User> GetUserAll();

        IList<User> GetUserPageList(int pageIndex = 0, int pageSize = 5);

        int ExecuteSql();
    }
}
