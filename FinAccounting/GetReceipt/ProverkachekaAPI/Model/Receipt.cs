using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace FinAccounting.GetReceipt.ProverkachekaAPI
{
    internal class Receipt
    {
        public string Organization;
        public DateTime DateTime;
        public double Total;
        public List<Item> Items;

        public Receipt()
        {
            Items = new List<Item>();
        }
    }
}
