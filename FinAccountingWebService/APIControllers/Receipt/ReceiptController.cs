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
        [HttpPost("recieptByRequisits")]
        public APIReceipt.Receipt ReceiptByRequisits(ReceiptRequesits requisits)
        {
            return APIProvider.GetReceiptByFiscalData(requisits.FiscalStorage,
                                                      requisits.FiscalDocument,
                                                      requisits.FiscalAttribute,
                                                      requisits.DateTime,
                                                      requisits.ReceiptType,
                                                      requisits.Total).Result;
        }

        [HttpPost("recieptByQRRaw")]
        public APIReceipt.Receipt ReceiptByQRRaw([FromBody] string qrraw)
        {
            return APIProvider.GetReceiptByQRRaw(qrraw).Result;
        }

        [HttpPost("recieptByQRUrl")]
        public APIReceipt.Receipt ReceiptByQRUrl([FromBody] string qrUrl)
        {
            return APIProvider.GetReceiptByQRUrl(qrUrl).Result;
        }
    }
}
