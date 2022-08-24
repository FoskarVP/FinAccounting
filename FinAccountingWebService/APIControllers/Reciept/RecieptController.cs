using FinAccountingWebService.APIReciept.ProverkachekaAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Text;

namespace FinAccountingWebService.APIControllers.Reciept
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecieptController : ControllerBase
    {
        [HttpPost("recieptByRequisits")]
        public APIReciept.Reciept RecieptByRequisits(RecieptRequesits requisits)
        {
            return APIProvider.GetReceiptByFiscalData(requisits.FiscalStorage,
                                                      requisits.FiscalDocument,
                                                      requisits.FiscalAttribute,
                                                      requisits.DateTime,
                                                      requisits.RecieptType,
                                                      requisits.Total).Result;
        }

        [HttpPost("recieptByQRRaw")]
        public APIReciept.Reciept RecieptByQRRaw([FromBody] string qrraw)
        {
            return APIProvider.GetReceiptByQRRaw(qrraw).Result;
        }

        [HttpPost("recieptByQRUrl")]
        public APIReciept.Reciept RecieptByQRUrl([FromBody] string qrUrl)
        {
            return APIProvider.GetReceiptByQRUrl(qrUrl).Result;
        }

        [HttpPost("recieptByQRImage")]
        public APIReciept.Reciept RecieptByQRImageStream()
        {
            return APIProvider.GetReceiptByQRImageStream(Request.Body).Result;
        }
    }
}
