using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FinAccounting.Database.Tables
{
    public class ReceiptPosition
    {
        public long ReceiptId { get; set; }
        public Receipt Receipt { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }

        public decimal Price { get; set; }
        public decimal Count { get; set; }

        public override string ToString()
        {
            return $"Product = \"{Product?.Title}\" | Receipt = {Receipt?.Datetime}, {Receipt?.Total} | Price = {Price} | Count = {Count} | Sum = {Price * Count}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is ReceiptPosition receiptPosition)
                return Product.Equals(receiptPosition.Product)
                    && Receipt.Equals(receiptPosition.Receipt)
                    && Price == receiptPosition.Price
                    && Count == receiptPosition.Count;
            
            return false;
        }
    }
}
