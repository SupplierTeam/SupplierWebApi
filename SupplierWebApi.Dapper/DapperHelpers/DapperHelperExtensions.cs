using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using DapperExtensions;
using MySql.Data.MySqlClient;
using SupplierWebApi.Dapper.Connections;

namespace SupplierWebApi.Dapper.DapperHelpers
{
    /// <summary>
    /// dapper 帮助类
    /// </summary>
    public static class DapperHelperExtensions
    {
        public static IDbTransaction TranStart(this MySqlConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            return connection.BeginTransaction();
        }
        public static void TranRollBack(this MySqlConnection connection, IDbTransaction tran)
        {
            tran.Rollback();
            if (connection.State == ConnectionState.Open)
                tran.Connection.Close();
        }
        public static void TranCommit(this MySqlConnection connection, IDbTransaction tran)
        {
            tran.Commit();
            if (connection.State == ConnectionState.Open)
                tran.Connection.Close();
        }

        public static bool Deleted<T>(this MySqlConnection connection, T obj, IDbTransaction tran = null, int? commandTimeout = null) where T : class
        {
            return connection.Delete(obj, tran, commandTimeout);
        }

        public static bool DeleteList<T>(this MySqlConnection connection, IEnumerable<T> list, IDbTransaction tran = null, int? commandTimeout = null) where T : class
        {
            return connection.Delete(list, tran, commandTimeout);
        }

        public static T GetOne<T>(this MySqlConnection connection, string id, IDbTransaction tran = null, int? commandTimeout = null) where T : class
        {
            return connection.Get<T>(id, tran, commandTimeout);
        }

        public static IEnumerable<T> GetAll<T>(this MySqlConnection connection, object predicate = null, IList<ISort> sort = null, IDbTransaction tran = null, int? commandTimeout = null, bool buffered = true) where T : class
        {
            return connection.GetList<T>(predicate, sort, tran, commandTimeout, buffered);
        }

        public static IEnumerable<T> GetPageList<T>(this MySqlConnection connection, object predicate, IList<ISort> sort, int page, int pageSize, IDbTransaction tran = null, int? commandTimeout = null, bool buffered = true) where T : class
        {
            return connection.GetPage<T>(predicate, sort, page, pageSize, tran, commandTimeout, buffered);
        }

        public static dynamic Add<T>(this MySqlConnection connection, T obj, IDbTransaction tran = null, int? commandTimeout = null) where T : class
        {
            return connection.Insert(obj, tran, commandTimeout);
        }

        public static void AddList<T>(this MySqlConnection connection, IEnumerable<T> list, IDbTransaction tran = null, int? commandTimeout = null) where T : class
        {
            connection.Insert(list, tran, commandTimeout);
        }

        public static bool Updated<T>(this MySqlConnection connection, T obj, IDbTransaction tran = null, int? commandTimeout = null, bool ignoreAllKeyProperties = true) where T : class
        {
            return connection.Update(obj, tran, commandTimeout, ignoreAllKeyProperties);
        }

        public static bool UpdateList<T>(this MySqlConnection connection, IEnumerable<T> list, IDbTransaction tran = null, int? commandTimeout = null, bool ignoreAllKeyProperties = true) where T : class
        {
            return connection.Update(list, tran, commandTimeout, ignoreAllKeyProperties);
        }

        public static List<T> SqlQuery<T>(this MySqlConnection connection, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return connection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType).AsList();
        }

        public static int Execute<T>(this MySqlConnection connection, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return connection.Execute(sql, param, transaction, commandTimeout, commandType);
        }
    }
}
