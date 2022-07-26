using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinAccounting
{
    public class MailSettings
    {
        string mailLogin = "s.polonski@mail.ru";
        string mailPassword = "cmeQnQSh29YTLpigpnpJ";
        string mailServer = "imap.mail.ru";
        int mailPort = 993;
        string mailReceiptFolder = "Чеки";

        public string MailLogin
        {
            get => mailLogin;
            set => mailLogin = value;
        }

        public string MailPassword
        {
            get => mailPassword;
            set => mailPassword = value;
        }

        public string MailServer
        {
            get => mailServer;
            set => mailServer = value;
        }

        public int MailPort
        {
            get => mailPort;
            set => mailPort = value;
        }

        public string MailReceiptFolder
        {
            get => mailReceiptFolder;
            set => mailReceiptFolder = value;
        }
    }
}
