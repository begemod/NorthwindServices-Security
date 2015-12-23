namespace DAL.QueryObjects
{
    public class UserQueryObject
    {
        public QueryObject GetUser(string userName, string password)
        {
            const string Sql = @"SELECT
                                  UserId
                                 ,UserName
                                FROM
                                  dbo.Users
                                WHERE
                                  UserName = @UserName
                                  AND Password = @Password";

            return new QueryObject(Sql, new { UserName = userName, Password = password });
        }
    }
}
