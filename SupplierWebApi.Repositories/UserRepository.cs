using Microsoft.EntityFrameworkCore;
using SupplierWebApi.IRepositories;
using SupplierWebApi.Models;
using SupplierWebApi.Models.DataContext;
using SupplierWebApi.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SupplierWebApi.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(SupplierdbContext supbackdbContext)
    : base(supbackdbContext)
        {
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="password">password</param>
        /// <returns>User</returns>
        public async Task<User> LoginAsync(string userName, string password)
        {
            return await db.User.Where(x => x.UserName.Equals(userName, StringComparison.Ordinal) && x.Password.Equals(password, StringComparison.Ordinal) && x.IsDelete == 0).FirstOrDefaultAsync();
        }

        public List<User> GetUser(int pageIndex, int limit, string userName, out int count)
        {
            var query = from a in db.User
                        where a.IsDelete == 0
                        && (string.IsNullOrEmpty(userName) || a.UserName.Contains(userName, StringComparison.Ordinal))
                        select new User
                        {
                            UserId = a.UserId,
                            Password = a.Password,
                            UserName = a.UserName,
                            IsDelete = a.IsDelete,
                            AddTime = a.AddTime,
                            Email = a.Email,
                            Remark = a.Remark,
                            TelPhone = a.TelPhone
                        };
            count = query.ToList().Count();
            return query.OrderByDescending(x => x.AddTime).Skip((pageIndex - 1) * limit).Take(limit).ToList();
        }

        /// <summary>
        /// 通过员工编号获取员工信息
        /// </summary>
        /// <param name="userId">员工编号</param>
        /// <returns>User</returns>
        public async Task<User> Find(string userId)
        {
            var query = from a in db.User
                        where a.UserId.Equals(userId, StringComparison.Ordinal)
                        && a.IsDelete == 0
                        select new User
                        {
                            UserId = a.UserId,
                            Password = a.Password,
                            UserName = a.UserName,
                            IsDelete = a.IsDelete,
                            AddTime = a.AddTime,
                            Email = a.Email,
                            Remark = a.Remark,
                            TelPhone = a.TelPhone
                        };
            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>bool</returns>
        public User EditUserInfo(User userInfo)
        {
            try
            {
                db.User.Attach(userInfo);
            }
            catch (Exception ex)
            {
                db.User.Add(userInfo);
              
            }

            db.Entry(userInfo).State = EntityState.Modified;
            db.Entry(userInfo).Property("UserId").IsModified = false;
            db.Entry(userInfo).Property("IsDelete").IsModified = false;
            db.Entry(userInfo).Property("AddTime").IsModified = false;

            if (db.SaveChanges() > 0)
            {
                return userInfo;
            }

            return null;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>bool</returns>
        public async Task<User> AddUserInfo(User userInfo)
        {
            userInfo.UserId = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);
            userInfo.AddTime = DateTime.Now;
            userInfo.IsDelete = 0;
            db.User.Add(userInfo);
            await db.SaveChangesAsync();
            return userInfo;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids">用户编号集合</param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteUserInfo(string[] ids)
        {
            bool result = false;
            List<User> users = await db.User.Where(x => x.IsDelete == 0 && ids.Contains(x.UserId)).ToListAsync();
            if (users.Count > 0)
            {
                users.ForEach(item =>
                {
                    // 删除用户
                    item.IsDelete = 1;
                    db.Entry(item).State = EntityState.Modified;
                });
                await db.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public async Task<bool> ReverseDeleteUserInfo(string[] ids)
        {
            bool result = false;
            List<User> users = await db.User.Where(x => x.IsDelete == 1 && ids.Contains(x.UserId)).ToListAsync();
            if (users.Count > 0)
            {
                users.ForEach(item =>
                {
                    // 撤销删除用户
                    item.IsDelete = 0;
                    db.Entry(item).State = EntityState.Modified;
                });
                await db.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public async Task<bool> DeleteUserTrue(string ids)
        {
            User user = await db.User.FirstOrDefaultAsync(x => x.IsDelete == 0 && x.UserId == ids);
            db.User.Remove(user);
            db.Entry(user).State = EntityState.Deleted;
            await db.SaveChangesAsync();
            return true;
        }
    }
}
