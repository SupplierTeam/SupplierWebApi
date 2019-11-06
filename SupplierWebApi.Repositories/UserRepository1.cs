using Microsoft.EntityFrameworkCore;
using SupplierWebApi.IRepositories;
using SupplierWebApi.Models;
using SupplierWebApi.Models.DataContext;
using SupplierWebApi.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy.Contributors;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using SupplierWebApi.Dapper.Connections;

namespace SupplierWebApi.Repositories
{
    public class UserRepository1 : BaseRepository1<User>, IUserRepository1
    {
        public User GetUserById(string id)
        {
            var entity = GetById(id);
            return entity;
        }

        public bool AddUser(User entity)
        {
            if (entity == null)
            {
                return false;
            }
            return Add(entity);
        }

        public bool UpdateUser(User entity)
        {
            if (entity == null)
            {
                return false;
            }

            return Update(entity);
        }

        public bool DeleteUser(User entity)
        {
            if (entity == null)
            {
                return false;
            }

            return Delete(entity);
        }

        public IList<User> GetUserAll()
        {
            return GetAll();
        }
    }
}
