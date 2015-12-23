namespace DAL.DataServices
{
    using System.Collections.Generic;
    using DAL.Entities;
    using DAL.QueryObjects;

    public class RolesDataService : BaseDataService
    {
        protected RolesDataService(IConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public IEnumerable<Role> GetUserRoles(int userId)
        {
            using (var connection = this.GetConnection())
            {
                var orderQueryObject = new RoleQueryObject();
                return connection.Query<Role>(orderQueryObject.GetUserRoles(userId));
            }
        }
    }
}
