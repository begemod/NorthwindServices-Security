namespace DAL.Entities
{
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public class Shipper
    {
        public Shipper()
        {
            this.Orders = new HashSet<Order>();
        }

        public int ShipperID { get; set; }

        [StringLength(40)]
        public string CompanyName { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
