using Microsoft.AspNetCore.Builder;

namespace FinAccountingWebService.APIControllers.Reciept
{
    public class RecieptRequesits
    {
        public string FiscalStorage { get; set; }
        public string FiscalDocument { get; set; }
        public string FiscalAttribute { get; set; }
        public DateTime DateTime { get; set; }
        public int RecieptType { get; set; }
        public decimal Total { get; set; }
    }
}
