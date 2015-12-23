namespace DAL.DataServices
{
    using System.Collections.Generic;
    using DAL.Entities;

    public class RolesDataService
    {
        public ICollection<Role> GetUserRoles(int userId)
        {
            return new List<Role>();
        }
    }
}
