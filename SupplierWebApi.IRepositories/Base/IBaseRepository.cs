using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SupplierWebApi.IRepositories.Base
{
    public interface IBaseRepository<TEntity>
    {
        TEntity FindById(string id);

        bool Add(TEntity entity);

        bool AddList(List<TEntity> entityList, int? commandTimeout = null);

        bool Update(TEntity entity);

        bool UpdateList(List<TEntity> entityList, int? commandTimeout = null);

        bool Delete(TEntity entity);

        IList<TEntity> GetList(IDbTransaction tran = null, int? commandTimeout = null);

        List<T> SqlQuery<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true,
            int? commandTimeout = null, CommandType? commandType = null);

        int ExecuteSql(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true,
            int? commandTimeout = null, CommandType? commandType = null);
    }
}
