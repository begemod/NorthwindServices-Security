namespace DAL.Entities
{
    using System.Collections.Generic;

    public class Role
    {
        public Role()
        {
            this.Users = new List<User>();
        }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
