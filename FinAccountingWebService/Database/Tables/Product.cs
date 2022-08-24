using System.ComponentModel.DataAnnotations;

namespace FinAccountingWebService.Database.Tables
{
    public class Product
    {
        public long ProductId { get; set; }
        public string Title { get; set; }
        
        public List<Receipt> Receipts{ get; set; } = new();
        public List<ReceiptPosition> ReceiptPositions { get; set; } = new();

        public override string ToString()
        {
            return Title;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Product product) return Title == product.Title;
            return false;
        }
    }
}
