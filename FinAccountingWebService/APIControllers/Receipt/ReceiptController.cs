using FinAccountingWebService.Database;
using FinAccountingWebService.APIReceipt.ProverkachekaAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Text;

namespace FinAccountingWebService.APIControllers.Receipt
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        [HttpPost("receiptByRequisits")]
        public APIReceipt.Receipt ReceiptByRequisits(ReceiptRequesits requisits)
        {
            var receipt = APIProvider.GetReceiptByFiscalData(requisits.FiscalStorage,
                                                      requisits.FiscalDocument,
                                                      requisits.FiscalAttribute,
                                                      requisits.DateTime,
                                                      requisits.ReceiptType,
                                                      requisits.Total).Result;
            DatabaseProvider.SaveReceipt(receipt);
            return receipt;
        }

        [HttpPost("receiptByQRRaw")]
        public APIReceipt.Receipt ReceiptByQRRaw([FromBody] string qrraw)
        {
            var receipt = APIProvider.GetReceiptByQRRaw(qrraw).Result;
            DatabaseProvider.SaveReceipt(receipt);
            return receipt;
        }

        [HttpPost("receiptByQRUrl")]
        public APIReceipt.Receipt ReceiptByQRUrl([FromBody] string qrUrl)
        {
            var receipt = APIProvider.GetReceiptByQRUrl(qrUrl).Result;
            DatabaseProvider.SaveReceipt(receipt);
            return receipt;
        }
    }
}
