using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace FinAccounting.APIReceipt
{
    public class Receipt
    {
        public string Organization;
        public DateTime DateTime;
        public decimal Total;
        public List<Item> Items;

        public Receipt()
        {
            Items = new List<Item>();
        }
    }
}
