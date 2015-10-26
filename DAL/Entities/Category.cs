namespace DAL.Entities
{
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }
        
        public int CategoryID { get; set; }

        [StringLength(15)]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
