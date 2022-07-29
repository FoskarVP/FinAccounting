// See https://aka.ms/new-console-template for more information
using FinAccounting;
using FinAccounting.Database;
using FinAccounting.GetReceipt.ProverkachekaAPI;
/*using MailKit.Net.Imap;
using MailKit;
using MailKit.Search;

MailClient mailClient = new(new MailSettings(), DateTime.Parse("2022-01-01"));

var a = mailClient.GetReceiptListFromEMail();*/

Console.WriteLine("Hello, World!");
AppSettings.SetSettings("config.json");

/*Product product = new Product() { title = "Test" };
using (DatabaseContext db = new DatabaseContext())
{
    var a = DatabaseProvider.GetProductByTitle("Test");
    db.product.Add(product);
    db.SaveChanges();
}
Console.WriteLine($"{product.product_id} - {product.title}");*/

var test1 = await APIProvider.GetReceiptByQRRaw("t=20220727T1157&s=5750.00&fn=9287440300906573&i=12896&fp=1927570358&n=1");
var test2 = await APIProvider.GetReceiptByFiscalData("9960440301884709",
                                               "27326",
                                               "1043864434",
                                               new DateTime(2022, 07, 27, 19, 58, 0),
                                               OperationTypeEnum.Income,
                                               164.72);



Console.WriteLine();