using SupplierWebApi.IRepositories.Base;
using SupplierWebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplierWebApi.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> LoginAsync(string userName, string password);

        /// <summary>
        /// 获取员工列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="limit">单页记录条数</param>
        /// <param name="userName">用户名</param>
        /// <param name="count">数据条数</param>
        /// <returns>List</returns>
        List<User> GetUser(int pageIndex, int limit, string userName, out int count);

        /// <summary>
        /// 通过与员工编号获取员工信息
        /// </summary>
        /// <param name="userId">员工编号</param>
        /// <returns>user</returns>
        Task<User> Find(string userId);

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>bool</returns>
        User EditUserInfo(User userInfo);

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>bool</returns>
        Task<User> AddUserInfo(User userInfo);

        Task<bool> DeleteUserInfo(string[] ids);

        Task<bool> ReverseDeleteUserInfo(string[] ids);

        Task<bool> DeleteUserTrue(string ids);
    }
}
