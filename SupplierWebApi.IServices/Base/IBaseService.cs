using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SupplierWebApi.IServices.Base
{
    public  interface IBaseService<TEntity>
    {
        /// <summary>
        /// 根据Id查询实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity FindById(string id);
        /// <summary>
        /// 增加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Add(TEntity entity);
        /// <summary>
        /// 增加实体集合
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        bool AddList(List<TEntity> entityList, int? commandTimeout = null);
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(TEntity entity);
        /// <summary>
        /// 更新实体集合
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        bool UpdateList(List<TEntity> entityList, int? commandTimeout = null);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Delete(TEntity entity);
        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        IList<TEntity> GetList(IDbTransaction tran = null, int? commandTimeout = null);
        /// <summary>
        /// sql查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        List<T> SqlQuery<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true,
            int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// 执行sql，返回影响条数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        int ExecuteSql(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true,
            int? commandTimeout = null, CommandType? commandType = null);
    }
}
