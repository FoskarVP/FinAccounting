using System.ComponentModel.DataAnnotations;

namespace FinAccounting.Database
{
    public class Shop
    {
        [Key]
        public long shop_id { get; set; }
        public string title { get; set; }
        public int shop_category_id { get; set; }
    }
}
