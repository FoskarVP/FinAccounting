using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace FinAccountingWebService.Database.Tables
{
    public class Shop
    {
        public long ShopId { get; set; }
        public string Title { get; set; }

        public List<Receipt> Receipts { get; set; } = new();

        public override string ToString()
        {
            return Title;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Shop shop) return Title == shop.Title;
            return false;
        }
    }
}
