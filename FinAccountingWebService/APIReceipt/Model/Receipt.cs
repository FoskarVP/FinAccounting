using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace FinAccountingWebService.APIReceipt
{
    public class Receipt
    {
        public string Organization { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Total { get; set; }
        public List<Item> Items { get; set; }

        public Receipt()
        {
            Items = new List<Item>();
        }
    }
}
