using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinAccounting.Mail.ReceiptModel
{
    internal class TaxcomReceipt : Receipt
    {
        public override List<ReceiptElement> GetProductListByReceiptText(string receiptText)
        {
            throw new NotImplementedException();
        }
    }
}
