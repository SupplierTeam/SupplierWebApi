using Microsoft.EntityFrameworkCore;
using SupplierWebApi.IRepositories.Base;
using SupplierWebApi.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SupplierWebApi.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T>
       where T : class
    {
#pragma warning disable SA1401 // Fields must be private
#pragma warning disable CA1051 // Do not declare visible instance fields
        protected SupplierdbContext db;
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1401 // Fields must be private

        public BaseRepository(SupplierdbContext supbackdbContext)
        {
            db = supbackdbContext;
        }

        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        public virtual bool Save(T entity)
        {
            db.Set<T>().Add(entity);
            return db.SaveChanges() > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        public virtual async Task<bool> SaveAsync(T entity)
        {
            await db.Set<T>().AddAsync(entity);
            return await db.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        public virtual bool Update(T entity)
        {
            try
            {
                db.Set<T>().Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 更新一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                db.Set<T>().Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                return await db.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通过Lamda表达式获取实体
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns>T</returns>
        public T Get(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// 通过Lamda表达式获取实体
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns>T</returns>
        public T GetNoTracking(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().AsNoTracking().FirstOrDefault(predicate);
        }

        /// <summary>
        /// 通过Lamda表达式获取实体（异步方式）
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns>T</returns>
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await db.Set<T>().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// 增加多条记录，同一模型
        /// </summary>
        /// <param name="list">实体模型集合</param>
        /// <returns>bool</returns>
        public virtual bool SaveList(List<T> list)
        {
            bool result = false;
            if (list == null || list.Count == 0)
            {
                return result;
            }

            using (var tran = db.Database.BeginTransaction())
            {
                try
                {
                    list.ForEach(item =>
                    {
                        db.Set<T>().Add(item);
                    });
                    db.SaveChanges();
                    tran.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                    tran.Rollback();
                    throw new Exception(ex.ToString());
                }
            }

            return result;
        }

        /// <summary>
        /// 增加多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="list">实体模型集合</param>
        /// <returns>bool</returns>
        public async Task<bool> SaveListAsync(List<T> list)
        {
            bool result = false;
            if (list == null || list.Count == 0)
            {
                return result;
            }

            using (var tran = db.Database.BeginTransaction())
            {
                try
                {
                    list.ForEach(item =>
                    {
                        db.Set<T>().Add(item);
                    });
                    await db.SaveChangesAsync();
                    tran.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                    tran.Rollback();
                    throw new Exception(ex.ToString());
                }
            }

            return result;
        }

        public List<T> List(Expression<Func<T, bool>> expression)
        {
            return db.Set<T>().Where(expression).ToList();
        }

        /// <summary>
		/// 更新多条记录，同一模型
		/// </summary>
		/// <param name="list">实体模型集合</param>
		/// <returns>bool</returns>
		public bool UpdateList(List<T> list)
        {
            bool result = false;
            if (list == null || list.Count == 0)
            {
                return result;
            }

            using (var tran = db.Database.BeginTransaction())
            {
                try
                {
                    list.ForEach(item =>
                    {
                        db.Set<T>().Update(item);
                    });
                    result = db.SaveChanges() > 0 ? true : false;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result = false;
                    tran.Rollback();
                    throw new Exception(ex.ToString());
                }
            }

            return result;
        }

        public List<T> ListAsnotracking(Expression<Func<T, bool>> expression)
        {
            return db.Set<T>().AsNoTracking().Where(expression).ToList();
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="expression">expression</param>
        /// <returns>int</returns>
        public int GetCount(Expression<Func<T, bool>> expression)
        {
            return db.Set<T>().Where(expression).Count();
        }

        /// <summary>
        /// 是否存在某条数据
        /// </summary>
        /// <param name="expression">lambda表达式</param>
        /// <returns>bool</returns>
        public bool IsExistence(Expression<Func<T, bool>> expression)
        {
            return db.Set<T>().Any(expression);
        }

        /// <summary>
        /// 执行mysql原生语句 
        /// </summary>
        /// <typeparam name="TEntity">查询返回结果的Dto</typeparam>
        /// <param name="mysql">MySQL语句</param>
        /// <param name="timeOutNum">超时时间</param>
        /// <param name="parameters">MySQLParameter 参数防止sql注入</param>
        /// <returns>返回结果Dto的集合</returns>
        public IList<TEntity> SqlQuery<TEntity>(string mysql, int? timeOutNum = null, params object[] parameters)
            where TEntity : new()
        {
            //注意：不要对GetDbConnection获取到的conn进行using或者调用Dispose，否则DbContext后续不能再进行使用了，会抛异常
            var conn = db.Database.GetDbConnection();
            try
            {
                conn.Open();
                using var command = conn.CreateCommand();
                if (timeOutNum.HasValue)
                {
                    command.CommandTimeout = timeOutNum.Value;
                }
                command.CommandText = mysql;
                if (parameters.Count() > 0)
                {
                    command.Parameters.AddRange(parameters);
                }

                var propts = typeof(TEntity).GetProperties();
                var rtnList = new List<TEntity>();
                TEntity model;
                object val;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model = new TEntity();
                        foreach (var l in propts)
                        {
                            val = reader[l.Name];
                            if (val == DBNull.Value)
                            {
                                l.SetValue(model, null);
                            }
                            else
                            {
                                l.SetValue(model, val);
                            }
                        }

                        rtnList.Add(model);
                    }
                }

                return rtnList;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
