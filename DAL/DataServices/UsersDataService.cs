namespace DAL.DataServices
{
    using DAL.Entities;

    public class UsersDataService
    {
        public User GetById(int userId)
        {
            return new User();
        }

        public bool IsExist(User user)
        {
            return true;
        }
    }
}
