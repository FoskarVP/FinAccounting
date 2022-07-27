using System.ComponentModel.DataAnnotations;

namespace FinAccounting.Database
{
    public class Product
    {
        [Key]
        public long product_id { get; set; }
        public string title { get; set; }
    }
}
