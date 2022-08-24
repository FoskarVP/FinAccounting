using Microsoft.AspNetCore.Builder;

namespace FinAccountingWebService.APIControllers.Receipt
{
    public class ReceiptRequesits
    {
        public string FiscalStorage { get; set; }
        public string FiscalDocument { get; set; }
        public string FiscalAttribute { get; set; }
        public DateTime DateTime { get; set; }
        public int ReceiptType { get; set; }
        public decimal Total { get; set; }
    }
}
