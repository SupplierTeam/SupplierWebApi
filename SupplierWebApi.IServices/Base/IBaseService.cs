using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SupplierWebApi.IServices.Base
{
   
    public interface IBaseService<T>
           where T : class, new()
    {
        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        bool Save(T entity);

        /// <summary>
        /// 增加一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        Task<bool> SaveAsync(T entity);

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        bool Update(T entity);

        /// <summary>
        /// 更新一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        Task<bool> UpdateAsync(T entity);

#pragma warning disable CA1716 // Identifiers should not match keywords
        /// <summary>
        /// 通过Lamda表达式获取实体
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns>T</returns>
        T Get(Expression<Func<T, bool>> predicate);
#pragma warning restore CA1716 // Identifiers should not match keywords

        /// <summary>
        /// 通过Lamda表达式获取实体（异步方式）
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns>T</returns>
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 增加多条记录，同一模型
        /// </summary>
        /// <param name="list">实体模型集合</param>
        /// <returns>bool</returns>
        bool SaveList(List<T> list);

        /// <summary>
        /// 增加多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="list">实体模型集合</param>
        /// <returns>bool</returns>
        Task<bool> SaveListAsync(List<T> list);

        List<T> List(Expression<Func<T, bool>> expression);

        List<T> ListAsnotracking(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="expression">expression</param>
        /// <returns>int</returns>
        int GetCount(Expression<Func<T, bool>> expression);
    }
}
