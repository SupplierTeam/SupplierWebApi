using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SupplierWebApi.Framework.Redis;
using SupplierWebApi.IRepositories;
using SupplierWebApi.IServices;
using SupplierWebApi.Models;
using SupplierWebApi.Models.Domain;


namespace SupplierWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        readonly IRedisCacheManager _redisCacheManager;

        public UserController(IUserService userService, IRedisCacheManager _redisCacheManager)
        {
            this.userService = userService;
            this._redisCacheManager = _redisCacheManager;
        }

        [HttpGet("GetUserById/{id}")]
        public ResultData GetUserById(string id)
        {
            //var exist = _redisCacheManager.KeyExists(id);
            //_redisCacheManager.Set(id,"111111", TimeSpan.FromSeconds(200));
            //var val = _redisCacheManager.GetValue(id);
              var model = userService.GetUserById(id);
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
            var list = userService.GetUserAll();
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
            var result = userService.AddUser(user);
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

        [HttpPost("AddUserList")]
        public ResultData AddUserList(List<User> userList)
        {
            if (userService.AddUserList(userList))
            {
                return new ResultData()
                {
                    Count = userList.Count,
                    Data = userList,
                    Code = 200,
                    Msg = ""
                };
            }
            else
            {
                return new ResultData()
                {
                    Count = 0,
                    Data = null,
                    Code = 500,
                    Msg = ""
                };
            }
        }

        [HttpPost("UpdateUser")]
        public ResultData UpdateUser(User user)
        {
            var result = userService.UpdateUser(user);
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

        [HttpPost("UpdateUserList")]
        public ResultData UpdateUserList(List<User> userList)
        {
            if (userService.UpdateUserList(userList))
            {
                return new ResultData()
                {
                    Count = userList.Count,
                    Data = userList,
                    Code = 200,
                    Msg = ""
                };
            }
            else
            {
                return new ResultData()
                {
                    Count = 0,
                    Data = null,
                    Code = 500,
                    Msg = ""
                };
            }
        }

        [HttpPost("DeleteUser")]
        public ResultData DeleteUser(User user)
        {
            var result = userService.AddUser(user);
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

        [HttpPost("QuerySql")]
        public ResultData QuerySql(int pageIndex, int pageSize)
        {
            var list = userService.GetUserPageList(pageIndex, pageSize);
            return new ResultData()
            {
                Count = 1,
                Data = list,
                Code = 200,
                Msg = ""
            };
        }

        [HttpPost("ExecuteSql")]
        public int ExecuteSql()
        {
            return userService.ExecuteSql();
        }

    }
}
