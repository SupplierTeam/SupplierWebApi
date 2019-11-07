using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SupplierWebApi.IRepositories;
using SupplierWebApi.Models;
using SupplierWebApi.Models.Domain;


namespace SupplierWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository1;

        public UserController(IUserRepository userRepository1)
        {
            this.userRepository1 = userRepository1;
        }

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

        [HttpPost("AddUserList")]
        public ResultData AddUserList(List<User> userList)
        {
            if (userRepository1.AddUserList(userList))
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
            var result = userRepository1.UpdateUser(user);
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
            if (userRepository1.UpdateUserList(userList))
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

        [HttpPost("QuerySql")]
        public ResultData QuerySql()
        {
            var list = userRepository1.GetUserPageList();
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
            return userRepository1.ExecuteSql();
        }

    }
}
