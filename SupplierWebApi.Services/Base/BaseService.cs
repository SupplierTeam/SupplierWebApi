using SupplierWebApi.IRepositories.Base;
using SupplierWebApi.IServices.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SupplierWebApi.Services.Base
{
  public  class BaseService<TEntity, TRepository> : IBaseService<TEntity>
        where TEntity:class
         where TRepository : IBaseRepository<TEntity>
    {
        protected TRepository Repository;

        public BaseService(TRepository Repository)
        {
            this.Repository = Repository;
        }

        /// <summary>
        /// 根据主键Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity FindById(string id)
        {
           return  Repository.FindById(id);
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Add(TEntity entity)
        {
            return Repository.Add(entity);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entityList"></param>   
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public bool AddList(List<TEntity> entityList, int? commandTimeout = null)
        {
            return Repository.AddList(entityList);
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(TEntity entity)
        {
            return Repository.Update(entity);
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public bool UpdateList(List<TEntity> entityList, int? commandTimeout = null)
        {
            return Repository.UpdateList(entityList);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(TEntity entity)
        {
            return Repository.Delete(entity);
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns></returns>
        public IList<TEntity> GetList(IDbTransaction tran = null, int? commandTimeout = null)
        {
            return Repository.GetList();
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
            return Repository.SqlQuery<T>(sql, param, transaction, buffered, commandTimeout, commandType);
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

            return Repository.ExecuteSql(sql, param, transaction);
          

        }
    }
}
