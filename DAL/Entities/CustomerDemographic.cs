namespace DAL.Entities
{
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public class CustomerDemographic
    {
        public CustomerDemographic()
        {
            this.Customers = new HashSet<Customer>();
        }

        [StringLength(10)]
        public string CustomerTypeID { get; set; }

        public string CustomerDesc { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
