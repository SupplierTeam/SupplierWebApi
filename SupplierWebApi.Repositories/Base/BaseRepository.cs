using SupplierWebApi.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
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
        protected SupDbConnetion _supDbConnetion;

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
        /// 获取分页数据 在不用存储过程情况下
        /// </summary>
        /// <param name="recordCount">总记录条数</param>
        /// <param name="selectList">选择的列逗号隔开,支持top num</param>
        /// <param name="tableName">表名字(多表，分割)</param>
        /// <param name="whereStr">条件字符 必须前加 and</param>
        /// <param name="orderExpression">排序 例如 ID</param>
        /// <param name="pageIndex">当前索引页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="param">参数（可用匿名类）</param>
        /// <returns></returns>
        public IList<TEntity> GetPageList(out int recordCount, string tableName, string whereStr, string orderExpression, int pageIndex, int pageSize, object param = null, string selectList = "*")
        {
            int rows = 0;
            var matchs = Regex.Matches(selectList, @"top\s+\d{1,}", RegexOptions.IgnoreCase);//含有top
            var sqlStr = $"select {selectList} from {tableName} where 1=1 {whereStr}";
            if (!string.IsNullOrEmpty(orderExpression)) { sqlStr += $" Order by {orderExpression}"; }

            using (var conn = _supDbConnetion.GetDbConnection())
            {
                if (matchs.Count > 0) //含有top的时候
                {
                    var dtTemp = conn.Query<TEntity>(sqlStr, param);
                    rows = dtTemp.Count();
                }
                else //不含有top的时候
                {
                    string sqlCount = $"select count(*) from {tableName} where 1=1 {whereStr} ";
                    //获取行数
                    object obj = conn.ExecuteScalar(sqlCount, param);
                    if (obj != null)
                    {
                        rows = Convert.ToInt32(obj);
                    }
                }

                sqlStr += $" limit {(pageIndex - 1) * pageSize},{pageSize}";
                recordCount = rows;
                return conn.Query<TEntity>(sqlStr, param).ToList();
            }
        }

        /// <summary>
        /// 执行sql（查询方法）
        /// </summary>
        /// <typeparam name="T"></typeparam>
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
