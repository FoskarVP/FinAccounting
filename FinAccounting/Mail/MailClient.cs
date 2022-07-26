using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using FinAccounting.Mail.ReceiptModel;

namespace FinAccounting
{
    public class MailClient
    {
        List<Receipt> receiptList;
        MailSettings settings;
        DateTime dateReceiptStart;

        public MailSettings Settings
        {
            get => settings;
            set => settings = value;
        }

        public MailClient()
        {
            receiptList = new();
            settings = new();
            dateReceiptStart = DateTime.Now;
        }

        public MailClient(MailSettings settings)
        {
            receiptList = new();
            this.settings = settings;
            dateReceiptStart = DateTime.Now;
        }

        public MailClient(MailSettings settings, DateTime dateReceiptStart)
        {
            receiptList = new();
            this.settings = settings;
            this.dateReceiptStart = dateReceiptStart;
        }

        public void ConnectAndAuthenticate(ImapClient client)
        {
            client.Connect(settings.MailServer, settings.MailPort, true);
            client.Authenticate(settings.MailLogin, settings.MailPassword);
        }

        public Receipt? GetReceipt()
        {
            return null;
        }

        public List<Receipt> GetReceiptListFromEMail()
        {
            using (ImapClient client = new())
            {
                ConnectAndAuthenticate(client);
                var receipetFolder = client.GetFolder(client.PersonalNamespaces[0]).GetSubfolder(settings.MailReceiptFolder);
                receipetFolder.Open(FolderAccess.ReadOnly);
                var query = SearchQuery.DeliveredAfter(dateReceiptStart.Date);

                foreach (var uid in receipetFolder.Search(query))
                {
                    var message = receipetFolder.GetMessage(uid);

                    Receipt receipt;
                    string messageFrom = message.From.ToString();
                    if (messageFrom.Contains("ofdreceipt@beeline.ru"))
                        receipt = new BeelineReceipt();
                    else if (messageFrom.Contains("no-reply@ofd.yandex.ru"))
                        receipt = new YandexReceipt();
                    else if (messageFrom.Contains("noreply@chek.pofd.ru"))
                        receipt = new POFDReceipt();
                    else if (messageFrom.Contains("noreply@taxcom.ru"))
                        receipt = new TaxcomReceipt();
                    else if (messageFrom.Contains("noreply@sbis.ru"))
                        receipt = new SBISReceipt();
                    else if (messageFrom.Contains("robot@konturcheck.ru"))
                        receipt = new TaxcomReceipt();

                    Console.WriteLine("[match] {0}: {1}", uid, message.BodyParts);
                }
            }
            return receiptList;
        }

    }
}
