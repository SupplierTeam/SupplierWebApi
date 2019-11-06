using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fairhr.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupplierWebApi.IServices;
using SupplierWebApi.Models;
using SupplierWebApi.Services;


namespace SupplierWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userLogicHandler)
        {
            userService = userLogicHandler;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
          

            if (!string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.Password))
            {
              
                bool result = false;
                string userId = await userService.LoginAsync(user.UserName, user.Password);
                if (!string.IsNullOrEmpty(userId))
                {
                    //CacheData data = new CacheData();
                    //data.UserId = userId.Trim('"');
                    //data.UserName = user.UserName;
                    //SessionHelper.SetAuthorityInfo(data);
                    result = true;
                }

                return Ok(result);
            }
            else
            {
                return BadRequest("请求参数不能为空。");
            }
        }
    }
}
