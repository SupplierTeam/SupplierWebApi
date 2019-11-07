using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;
using SupplierWebApi.Dapper.Connections;

namespace SupplierWebApi.Dapper.DapperHelpers
{
    /// <summary>
    /// dapper 帮助类
    /// </summary>
    public static class DapperHelperExtensions
    {
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static IDbTransaction TranStart(this MySqlConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            return connection.BeginTransaction();
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="tran"></param>
        public static void TranRollBack(this MySqlConnection connection, IDbTransaction tran)
        {
            tran.Rollback();
            if (connection.State == ConnectionState.Open)
                tran.Connection.Close();
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="tran"></param>
        public static void TranCommit(this MySqlConnection connection, IDbTransaction tran)
        {
            tran.Commit();
            if (connection.State == ConnectionState.Open)
                tran.Connection.Close();
        }

    }
}
