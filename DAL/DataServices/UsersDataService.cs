namespace DAL.DataServices
{
    using System.Linq;
    using DAL.Entities;
    using DAL.QueryObjects;

    public class UsersDataService : BaseDataService
    {
        public UsersDataService(IConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public User GetByUserName(string userName, string password)
        {
            using (var connection = GetConnection())
            {
                var queryObject = new UserQueryObject();

                return connection.Query<User>(queryObject.GetUser(userName, password)).FirstOrDefault();
            }
        }
    }
}
