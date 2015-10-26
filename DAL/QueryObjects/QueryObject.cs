namespace DAL.QueryObjects
{
    using System;

    public class QueryObject
    {
        public QueryObject(string sql)
        {
            if (string.IsNullOrEmpty(sql))
            {
                throw new ArgumentNullException("sql");
            }

            this.Sql = sql;
        }

        public QueryObject(string sql, object queryParams)
            : this(sql)
        {
            this.QueryParams = queryParams;
        }

        public string Sql { get; private set; }

        public object QueryParams { get; private set; }
    }
}