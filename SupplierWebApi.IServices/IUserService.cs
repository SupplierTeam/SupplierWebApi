using SupplierWebApi.IServices.Base;
using SupplierWebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplierWebApi.IServices
{
    public interface IUserService : IBaseService<User>
    {
        Task<string> LoginAsync(string userName, string password);

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
        /// 根据员工ID查询员工信息
        /// </summary>
        /// <param name="userId">员工编号</param>
        /// <returns>User</returns>
        Task<User> Find(string userId);

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>bool</returns>
        Task<bool> EditUserInfoAsync(User userInfo);

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>bool</returns>
        Task<bool> AddUserInfo(User userInfo);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids">用户集合</param>
        /// <returns>删除信息提示</returns>
        Task<string> DeleteUserInfo(string[] ids);

        /// <summary>
        /// 撤销删除用户操作
        /// </summary>
        /// <param name="ids">用户集合</param>
        /// <returns>string</returns>
        Task<string> ReverseDeleteUserInfo(string[] ids);

        Task<bool> DeleteUserTrue(string ids);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="newPassword">newPassword</param>
        /// <returns>bool</returns>
        bool EditUser(string newPassword);
    }
}
