namespace DAL.QueryObjects
{
    public class RoleQueryObject
    {
        public QueryObject GetUserRoles(int userId)
        {
            const string Sql = @"SELECT
                                  r.RoleId
                                 ,r.RoleName
                                 ,r.Description
                                FROM 
                                  dbo.Roles r
                                  JOIN dbo.UserRoles ur ON ur.RoleId = r.RoleId
                                WHERE
                                  ur.UserId = @UserId";

            return new QueryObject(Sql, new { UserId = userId });
        }
    }
}
