using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace FinAccountingWebService.APIReciept
{
    public class Reciept
    {
        public string Organization { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Total { get; set; }
        public List<Item> Items { get; set; }

        public Reciept()
        {
            Items = new List<Item>();
        }
    }
}
