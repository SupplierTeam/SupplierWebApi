using SupplierWebApi.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using SupplierWebApi.Dapper.Connections;
using SupplierWebApi.Dapper.DapperHelpers;

namespace SupplierWebApi.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
       where TEntity : class
    {
        private SupDbConnetion _supDbConnetion;

        public BaseRepository()
        {
            _supDbConnetion = new SupDbConnetion();
        }

        /// <summary>
        /// 根据主键Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity FindById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            using (var conn = _supDbConnetion.GetDbConnection())
            {
                return conn.Get<TEntity>(id);
            }
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Add(TEntity entity)
        {
            if (entity == null)
            {
                return false;
            }

            using (var conn = _supDbConnetion.GetDbConnection())
            {
                var identity = conn.Insert(entity);
                return identity > 0;
            }
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entityList"></param>   
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public bool AddList(List<TEntity> entityList, int? commandTimeout = null)
        {
            if (!entityList.Any())
            {
                return false;
            }
            var result = true;
            using (var conn = _supDbConnetion.GetDbConnection())
            {
                //开启事务
                using (var tran = conn.TranStart())
                {
                    try
                    {
                        var identity = conn.Insert(entityList, tran, commandTimeout);
                        result = identity > 0;
                        conn.TranCommit(tran);
                    }
                    catch (Exception e)
                    {
                        conn.TranRollBack(tran);
                        result = false;
                        throw new Exception(e.Message);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(TEntity entity)
        {
            if (entity == null)
            {
                return false;
            }
            using (var conn = _supDbConnetion.GetDbConnection())
            {
                return conn.Update(entity);
            }
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public bool UpdateList(List<TEntity> entityList, int? commandTimeout = null)
        {
            if (!entityList.Any())
            {
                return false;
            }
            var result = true;
            using (var conn = _supDbConnetion.GetDbConnection())
            {
                //开启事务
                using (var tran = conn.TranStart())
                {
                    try
                    {
                        result = conn.Update(entityList, tran);
                        conn.TranCommit(tran);
                    }
                    catch (Exception e)
                    {
                        conn.TranRollBack(tran);
                        result = false;
                        throw new Exception(e.Message);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(TEntity entity)
        {
            if (entity == null)
            {
                return false;
            }
            using (var conn = _supDbConnetion.GetDbConnection())
            {
                return conn.Delete(entity);
            }
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns></returns>
        public IList<TEntity> GetList(IDbTransaction tran = null, int? commandTimeout = null)
        {
            using (var conn = _supDbConnetion.GetDbConnection())
            {
                return conn.GetAll<TEntity>(tran, commandTimeout).ToList();
            }
        }

        /// <summary>
        /// 执行sql（查询方法）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="sql">sql</param>
        /// <param name="param">param</param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public List<T> SqlQuery<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var conn = _supDbConnetion.GetDbConnection())
            {
                return conn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType).AsList();
            }
        }

        /// <summary>
        /// 执行sql（返回受影响的行数）
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public int ExecuteSql(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var conn = _supDbConnetion.GetDbConnection())
            {
                return conn.Execute(sql, param, transaction, commandTimeout, commandType);
            }

        }
    }
}
