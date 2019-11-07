using SupplierWebApi.IServices.Base;
using SupplierWebApi.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierWebApi.IServices
{
    public interface IUserService : IBaseService<User>
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
