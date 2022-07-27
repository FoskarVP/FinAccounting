using System.ComponentModel.DataAnnotations;

namespace FinAccounting.Database
{
    public class Receipt
    {
        [Key]
        public long receipt_id { get; set; }
        public DateTime datetime { get; set; }
        public double total { get; set; }
        public long shop_id { get; set; }
    }
}
