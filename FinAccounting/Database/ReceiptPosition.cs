using System.ComponentModel.DataAnnotations;

namespace FinAccounting.Database
{
    public class ReceiptPosition
    {
        [Key]
        public long receipt_position_id { get; set; }
        public long receipt_id { get; set; }
        public long product_id { get; set; }
        public double price { get; set; }
        public double count { get; set; }
        public double sum { get; set; }
    }
}
