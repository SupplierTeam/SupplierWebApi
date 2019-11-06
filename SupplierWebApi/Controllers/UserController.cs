using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fairhr.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupplierWebApi.IRepositories;
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
        private readonly IUserRepository1 userRepository1;

        public UserController(IUserService userLogicHandler, IUserRepository1 userRepository1)
        {
            userService = userLogicHandler;
            this.userRepository1 = userRepository1;
        }

        ///// <summary>
        ///// 登录
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //[HttpPost("login")]
        //public async Task<IActionResult> Login(User user)
        //{
        //throw new Exception("login error");
        //bool result = false;
        //string userId = await userService.LoginAsync(user.UserName, user.Password);
        //    if (!string.IsNullOrEmpty(userId))

        //    if (!string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.Password))
        //    {
        //        //throw new Exception("login error");
        //        bool result = false;
        //        string userId = await userService.LoginAsync(user.UserName, user.Password);
        //        if (!string.IsNullOrEmpty(userId))
        //        {
        //            //CacheData data = new CacheData();
        //            //data.UserId = userId.Trim('"');
        //            //data.UserName = user.UserName;
        //            //SessionHelper.SetAuthorityInfo(data);
        //            result = true;
        //        }

        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return BadRequest("请求参数不能为空。");
        //    }
        //}

        [HttpGet("GetUserById/{id}")]
        public ResultData GetUserById(string id)
        {
            var model = userRepository1.GetUserById(id);
            return new ResultData()
            {
                Count = 1,
                Data = model,
                Code = 200,
                Msg = ""
            };
        }

        [HttpGet("GetUserAll")]
        public ResultData GetUserAll()
        {
            var list = userRepository1.GetUserAll();
            return new ResultData()
            {
                Count = 1,
                Data = list,
                Code = 200,
                Msg = ""
            };
        }

        [HttpPost("AddUser")]
        public ResultData AddUser(User user)
        {
            var result = userRepository1.AddUser(user);
            if (result)
            {
                return new ResultData()
                {
                    Count = 1,
                    Data = result,
                    Code = 200,
                    Msg = ""
                };
            }
            else
            {
                return new ResultData()
                {
                    Count = 1,
                    Data = result,
                    Code = 500,
                    Msg = ""
                };
            }
        }

        [HttpPost("DeleteUser")]
        public ResultData DeleteUser(User user)
        {
            var result = userRepository1.AddUser(user);
            if (result)
            {
                return new ResultData()
                {
                    Count = 1,
                    Data = result,
                    Code = 200,
                    Msg = ""
                };
            }
            else
            {
                return new ResultData()
                {
                    Count = 1,
                    Data = result,
                    Code = 500,
                    Msg = ""
                };
            }
        }

    }
}
