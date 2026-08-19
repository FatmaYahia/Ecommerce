using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.AppAdmin.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("brandId")]
        public virtual Brand Brand { get; set; }

        public int brandId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        
        public int CategoryId { get; set; }
    }
}
