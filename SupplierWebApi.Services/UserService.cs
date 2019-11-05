
using SupplierWebApi.Framework;
using SupplierWebApi.IRepositories;
using SupplierWebApi.IServices;
using SupplierWebApi.Models;
using SupplierWebApi.Models.Dto;
using SupplierWebApi.Services.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SupplierWebApi.Services
{
   

    public class UserService : BaseService<User, IUserRepository>, IUserService
    {
        public IConfiguration Configuration { get; }
        public UserService(IUserRepository Repository, IConfiguration theconfiguration) : base(Repository)
        {
            this.Configuration = theconfiguration;
        }

        public List<User> GetUser(int pageIndex, int limit, string userName, out int count)
        {
            return this.BaseRepositories.GetUser(pageIndex, limit, userName, out count);
        }

        public async Task<string> LoginAsync(string userName, string password)
        {
            string result = string.Empty;
            User user = await this.BaseRepositories.LoginAsync(userName, MD5Helper.MD5Hash(password));
            if (user != null)
            {
                // 访问权限系统登陆接口
                try
                {
                    LoginDto info = new LoginDto()
                    {
                        UserName = userName,
                        Password = password,
                        SystemId = Configuration.GetSection("AppSettings")["systemId"]
                    };
                    string url = Configuration.GetSection("AppSettings")["AuthorithApiAddress"] + "/api/Public/Login";
                    Uri uri = new Uri(url);

                    result = JsonHelper.NewtonsoftDeserialize<LoginUserDto>(HttpRequestHelper.HttpPostJson(uri, JsonHelper.NewtonsoftSerialiize(info))).UserId;

                }
                catch (Exception)
                {
                    return result;
                }
            }
            else
            {
                return result;
            }

            return result;


        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="newPassword">newPassword</param>
        /// <returns>bool</returns>
        public bool EditUser(string newPassword)
        {
           
            return  false;
        }

        /// <summary>
        /// 通过用户编号查找用户信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>User</returns>
        public async Task<User> Find(string userId)
        {
            return await this.BaseRepositories.Find(userId);
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>bool</returns>
        public async Task<bool> EditUserInfoAsync(User userInfo)
        {

            return await Task.Run(()=> false); 
           
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>bool</returns>
        public async Task<bool> AddUserInfo(User userInfo)
        {

            return await Task.Run(() => false);

        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="ids">用户编号集合</param>
        /// <returns>string</returns>
        public async Task<string> DeleteUserInfo(string[] ids)
        {
            string result = string.Empty;


            return await Task.Run(() => result);
        }

        /// <summary>
        /// 撤销删除操作（用于当权限系统删除失败时，撤销对业务协作中心的删除操作）
        /// </summary>
        /// <param name="ids">用户ID集合</param>
        /// <returns>string</returns>
        public async Task<string> ReverseDeleteUserInfo(string[] ids)
        {
            bool data = await this.BaseRepositories.DeleteUserInfo(ids);
            string result;
            if (data)
            {
                result = "反向删除成功。";
            }
            else
            {
                result = "反向删除失败。";
            }

            return result;
        }

        /// <summary>
        /// 彻底删除用户（适用于新增用户时业务协作中心添加成功而权限中心添加失败时要删除业务中心的数据）
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteUserTrue(string ids)
        {
            bool data = await this.BaseRepositories.DeleteUserTrue(ids);
            return data;
        }
    }
}
