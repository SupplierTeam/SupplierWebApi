using SupplierWebApi.IRepositories;
using SupplierWebApi.IServices;
using SupplierWebApi.Models.Domain;
using SupplierWebApi.Services.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SupplierWebApi.Services
{
    public class UserService :  BaseService<User, IUserRepository>, IUserService
    {

        public UserService(IUserRepository Repository) : base(Repository)
        {

        }

        public bool AddUser(User entity)
        {
            return this.Repository.AddUser(entity);
        }

        public bool AddUserList(List<User> entityList)
        {
            return this.Repository.AddUserList(entityList);
        }

        public bool DeleteUser(string id)
        {
            return this.Repository.DeleteUser(id);
        }

        public int ExecuteSql()
        {
            return this.Repository.ExecuteSql();
        }

        public IList<User> GetUserAll()
        {
            return this.Repository.GetUserAll();
        }

        public User GetUserById(string id)
        {
            return this.Repository.GetUserById(id);
        }

        public IList<User> GetUserPageList(int pageIndex = 0, int pageSize = 5)
        {
           return this.Repository.GetUserPageList();
        }

        public bool UpdateUser(User entity)
        {
            return this.Repository.UpdateUser(entity);
        }

        public bool UpdateUserList(List<User> entityList)
        {
            return this.Repository.UpdateUserList(entityList);
        }
    }
}
