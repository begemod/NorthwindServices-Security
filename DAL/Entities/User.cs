namespace DAL.Entities
{
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Roles = new List<Role>();
        }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
