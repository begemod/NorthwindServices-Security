namespace DAL.Entities
{
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public class Territory
    {
        public Territory()
        {
            this.Employees = new HashSet<Employee>();
        }

        [StringLength(20)]
        public string TerritoryID { get; set; }

        [StringLength(50)]
        public string TerritoryDescription { get; set; }

        public int RegionID { get; set; }

        public Region Region { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
