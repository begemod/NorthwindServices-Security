namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using DAL.QueryObjects;
    using Dapper;

    public static class DapperQueryObjectExtensions
    {
        public static IEnumerable<T> Query<T>(this IDbConnection cnn, QueryObject queryObject, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.Query<T>(queryObject.Sql, queryObject.QueryParams, transaction, buffered, commandTimeout, commandType);
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(this IDbConnection cnn, QueryObject queryObject, Func<TFirst, TSecond, TReturn> map, string splitOn = "Id", IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.Query(queryObject.Sql, map, queryObject.QueryParams, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static int Execute(this IDbConnection cnn, QueryObject queryObject, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.Execute(queryObject.Sql, queryObject.QueryParams, transaction, commandTimeout, commandType);
        }
    }
}