namespace DAL.DataServices
{
    using System.Linq;
    using DAL.Entities;
    using DAL.Infrastructure;
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

                var user = connection.Query<User>(queryObject.GetUser(userName, password)).SingleOrDefault();

                if (user == null)
                {
                    throw new EntityNotFoundException("User by defined name and password does not exist.");
                }

                return user;
            }
        }
    }
}
