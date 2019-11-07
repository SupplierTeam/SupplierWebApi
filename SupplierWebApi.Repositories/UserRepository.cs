using SupplierWebApi.IRepositories;
using SupplierWebApi.Models;
using SupplierWebApi.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplierWebApi.Dapper.Connections;
using SupplierWebApi.Models.Domain;

namespace SupplierWebApi.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        /// <summary>
        /// 根据主键Id获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserById(string id)
        {
            var entity = FindById(id);
            return entity;
        }

        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AddUser(User entity)
        {
            if (entity == null)
            {
                return false;
            }
            return Add(entity);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public bool AddUserList(List<User> entityList)
        {
            if (!entityList.Any())
            {
                return false;
            }

            return AddList(entityList);
        }

        /// <summary>
        /// 编辑员工
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateUser(User entity)
        {
            if (entity == null)
            {
                return false;
            }

            return Update(entity);
        }

        /// <summary>
        /// 批量编辑
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public bool UpdateUserList(List<User> entityList)
        {
            if (!entityList.Any())
            {
                return false;
            }

            return UpdateList(entityList);
        }

        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            return Delete(new User { UserId = id });
        }

        public IList<User> GetUserAll()
        {
            return GetList();
        }

        public IList<User> GetUserPageList()
        {
            var sqlQuery = "select * from user";
            return SqlQuery<User>(sqlQuery);
        }

        public int ExecuteSql()
        {
            var sqlQuery = "select * from user";
            return ExecuteSql(sqlQuery);
        }
    }
}
