using Microsoft.EntityFrameworkCore;
using SupplierWebApi.IRepositories.Base;
using SupplierWebApi.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using SupplierWebApi.Dapper.Connections;
using SupplierWebApi.Dapper.DapperHelpers;

namespace SupplierWebApi.Repositories.Base
{
    public class BaseRepository1<TEntity> : IBaseRepository1<TEntity>
       where TEntity : class
    {
        private SupDbConnetion _supDbConnetion;

        public BaseRepository1()
        {
            _supDbConnetion = new SupDbConnetion();
        }

        /// <summary>
        /// 根据主键Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetById(string id)
        {
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
            using (var conn = _supDbConnetion.GetDbConnection())
            {
                return conn.Insert(entity);
            }
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public bool AddList(List<TEntity> entityList)
        {
            var result = true;
            using (var conn = _supDbConnetion.GetDbConnection())
            {
                //开启事务
                using (var tran = conn.TranStart())
                {
                    try
                    {
                        conn.Insert(entityList, tran);
                        result = true;
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
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
            using (var conn = _supDbConnetion.GetDbConnection())
            {
                return conn.Update(entity);
            }
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public bool UpdateList(List<TEntity> entityList)
        {
            var result = true;
            using (var conn = _supDbConnetion.GetDbConnection())
            {
                //开启事务
                using (var tran = conn.TranStart())
                {
                    try
                    {
                        conn.Update(entityList, tran);
                        result = true;
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
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
            using (var conn = _supDbConnetion.GetDbConnection())
            {
                return conn.Delete(entity);
            }
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns></returns>
        public List<TEntity> GetAll()
        {
            using (var conn = _supDbConnetion.GetDbConnection())
            {
                return conn.GetAll<TEntity>().ToList();
            }
        }

        /// <summary>
        /// 分页获取实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sorts"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<TEntity> GetPageList(IFieldPredicate predicate, IList<ISort> sorts, int pageIndex, int pageSize)
        {
            using (var conn = _supDbConnetion.GetDbConnection())
            {
                //var predicate = Predicates.Field<T>(f => f.FNAME, Operator.Eq, "test2");
                return conn.GetPage<TEntity>(predicate, sorts, pageIndex, pageSize).ToList();
            }
        }
    }
}
