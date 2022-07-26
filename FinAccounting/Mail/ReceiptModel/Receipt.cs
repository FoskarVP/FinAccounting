using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinAccounting.Mail.ReceiptModel
{
    public struct ReceiptElement
    {
        string name;
        double quantity;
        double amount;
    }

    public abstract class Receipt
    {
        List<ReceiptElement> productList;
        DateTime receiptDateTime;
        string store;

        public List<ReceiptElement> ProductList { get; private set; }
        public DateTime ReceiptDateTime { get; private set; }
        public string Store { get; private set; }

        public abstract List<ReceiptElement> GetProductListByReceiptText(string receiptText);

    }
}
