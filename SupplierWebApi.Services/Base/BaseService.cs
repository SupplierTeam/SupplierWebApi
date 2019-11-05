using SupplierWebApi.IRepositories.Base;
using SupplierWebApi.IServices.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SupplierWebApi.Services.Base
{
  

    public class BaseService<T, TRepository> : IBaseService<T>
           where T : class, new()
           where TRepository : IBaseRepository<T>
    {
        protected TRepository BaseRepositories;

        public BaseService(TRepository Repository)
        {
            this.BaseRepositories = Repository;
        }

        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        public bool Save(T entity)
        {
            return BaseRepositories.Save(entity);
        }

        /// <summary>
        /// 增加一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        public async Task<bool> SaveAsync(T entity)
        {
            return await BaseRepositories.SaveAsync(entity);
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        public bool Update(T entity)
        {
            return BaseRepositories.Update(entity);
        }

        /// <summary>
        /// 更新一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateAsync(T entity)
        {
            return await BaseRepositories.UpdateAsync(entity);
        }

#pragma warning disable CA1716 // Identifiers should not match keywords
        /// <summary>
        /// 通过Lamda表达式获取实体
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns>T</returns>
        public T Get(Expression<Func<T, bool>> predicate)
        {
            return BaseRepositories.Get(predicate);
        }
#pragma warning restore CA1716 // Identifiers should not match keywords

        /// <summary>
        /// 通过Lamda表达式获取实体（异步方式）
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns>T</returns>
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await BaseRepositories.GetAsync(predicate);
        }

        /// <summary>
        /// 增加多条记录，同一模型
        /// </summary>
        /// <param name="list">实体模型集合</param>
        /// <returns>bool</returns>
        public bool SaveList(List<T> list)
        {
            return BaseRepositories.SaveList(list);
        }

        /// <summary>
        /// 增加多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="list">实体模型集合</param>
        /// <returns>bool</returns>
        public async Task<bool> SaveListAsync(List<T> list)
        {
            return await BaseRepositories.SaveListAsync(list);
        }

        public List<T> List(Expression<Func<T, bool>> expression)
        {
            return BaseRepositories.List(expression);
        }

        public List<T> ListAsnotracking(Expression<Func<T, bool>> expression)
        {
            return BaseRepositories.ListAsnotracking(expression);
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="expression">expression</param>
        /// <returns>int</returns>
        public int GetCount(Expression<Func<T, bool>> expression)
        {
            return BaseRepositories.GetCount(expression);
        }
    }
}
