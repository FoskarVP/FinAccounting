namespace FinAccountingWebService.Database.Tables
{
    public class Receipt
    {
        public long ReceiptId { get; set; }
        public DateTime Datetime { get; set; }
        public decimal Total { get; set; }

        public long ShopId { get; set; }
        public Shop? Shop { get; set; }

        public List<Product> Products { get; set; } = new();
        public List<ReceiptPosition> ReceiptPositions { get; set; } = new();

        public override string ToString()
        {
            return $"Datetime = {Datetime} | Total = {Total} | Shop = {Shop?.Title}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Receipt receipt) 
                return Datetime == receipt.Datetime
                    && Total == receipt.Total
                    && Shop.Equals(receipt.Shop);

            return false;
        }
    }
}
