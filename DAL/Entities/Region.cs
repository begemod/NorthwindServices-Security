namespace DAL.Entities
{
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public class Region
    {
        public Region()
        {
            this.Territories = new HashSet<Territory>();
        }

        public int RegionID { get; set; }

        [StringLength(50)]
        public string RegionDescription { get; set; }

        public ICollection<Territory> Territories { get; set; }
    }
}
