// See https://aka.ms/new-console-template for more information
using FinAccounting;
using FinAccounting.Database;
using FinAccounting.Database.Tables;
using FinAccounting.APIReceipt;
using FinAccounting.APIReceipt.ProverkachekaAPI;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

/*using MailKit.Net.Imap;
using MailKit;
using MailKit.Search;

MailClient mailClient = new(new MailSettings(), DateTime.Parse("2022-01-01"));

var a = mailClient.GetReceiptListFromEMail();*/

Console.WriteLine("Hello, World!");
AppSettings.SetSettings("config.json");


Product product1 = new Product() { Title = "Банан" };
Product product2 = new Product() { Title = "Молоко" };
Product product3 = new Product() { Title = "Чай" };
Product product4 = new Product() { Title = "Мышка" };
Product product5 = new Product() { Title = "Клавиатура" };

Shop shop1 = new Shop() { Title = "5ka" };
Shop shop2 = new Shop() { Title = "DNS" };


var receipt1 = new FinAccounting.Database.Tables.Receipt() { Datetime = new DateTime(2022, 08, 30, 12, 15, 19, DateTimeKind.Local).ToUniversalTime(), Total = 666.66m, Shop = shop1 };
var receipt2 = new FinAccounting.Database.Tables.Receipt() { Datetime = new DateTime(2022, 07, 20, 12, 15, 19, DateTimeKind.Local).ToUniversalTime(), Total = 246.50m, Shop = shop2 };

ReceiptPosition receiptPosition1 = new ReceiptPosition() { Receipt = receipt1, Product = product1, Price = 111.11m, Count = 2 };
ReceiptPosition receiptPosition2 = new ReceiptPosition() { Receipt = receipt1, Product = product2, Price = 222.22m, Count = 1 };
ReceiptPosition receiptPosition3 = new ReceiptPosition() { Receipt = receipt1, Product = product3, Price = 333.33m, Count = 1 };
ReceiptPosition receiptPosition4 = new ReceiptPosition() { Receipt = receipt2, Product = product4, Price = 106.20m, Count = 1 };
ReceiptPosition receiptPosition5 = new ReceiptPosition() { Receipt = receipt2, Product = product5, Price = 140.30m, Count = 1 };

using (DatabaseContext db = new DatabaseContext(true))
{
    db.Product.RemoveRange(db.Product.ToList());
    db.Shop.RemoveRange(db.Shop.ToList());
    db.Receipt.RemoveRange(db.Receipt.ToList());
    db.ReceiptPosition.RemoveRange(db.ReceiptPosition.ToList());

    db.Shop.AddRange(shop1, shop2);
    db.Product.AddRange(product1, product2, product3, product4);
    db.Receipt.AddRange(receipt1, receipt2);

    db.Shop.AddIfNotExists(shop1);
    db.Product.AddIfNotExists(product4);
    db.Product.AddIfNotExists(product5);

    db.SaveChanges();

    /*    receipt1.ReceiptPositions.Add( new ReceiptPosition() { Product = product1, Price = 111.11m, Count = 2 });
        receipt1.ReceiptPositions.Add( new ReceiptPosition() { Product = product2, Price = 222.22m, Count = 1 });
        receipt1.ReceiptPositions.Add( new ReceiptPosition() { Product = product3, Price = 333.33m, Count = 1 });
        receipt2.ReceiptPositions.Add( new ReceiptPosition() { Product = product4, Price = 106.20m, Count = 1 });
        receipt2.ReceiptPositions.Add( new ReceiptPosition() { Product = product5, Price = 140.30m, Count = 1 });*/

    //db.ReceiptPosition.AddRange(receiptPosition1, receiptPosition2, receiptPosition3, receiptPosition4, receiptPosition5);
}

/*using (DatabaseContext db = new DatabaseContext())
{
    var shops = db.Shop.ToList();
    var products = db.Product.ToList();
    var receipts = db.Receipt.ToList();
    var receiptPositions = db.ReceiptPosition.ToList();

    foreach (var shop in shops)
    {
        Console.WriteLine(shop);
        foreach (var receipt in shop.Receipts)
        {
            Console.Write("\t");
            Console.WriteLine(receipt);
            foreach (var receiptPosition in receipt.ReceiptPositions)
            {
                Console.Write("\t\t");
                Console.WriteLine(receiptPosition);
            }
        }
    }*/

/*    var receiptPositions = db.ReceiptPosition.Include(rp => rp.Product).Include(rp => rp.Receipt).ThenInclude(r => r.Shop);

    Console.WriteLine("====================================================");
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();
    foreach (var receiptPosition in receiptPositions)
    {
        Console.WriteLine("====================================================");

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();

        Console.WriteLine(receiptPosition.Receipt.Shop);
        Console.WriteLine("\t" + receiptPosition.Receipt);
        Console.WriteLine("\t\t" + receiptPosition);
        Console.WriteLine("\t\t\t" + receiptPosition.Product);

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("====================================================");

        Console.WriteLine();
    }

    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("====================================================");


}*/

var test1 = await APIProvider.GetReceiptByQRRaw("t=20220727T1157&s=5750.00&fn=9287440300906573&i=12896&fp=1927570358&n=1");
var test2 = await APIProvider.GetReceiptByFiscalData("9960440301884709",
                                               "27326",
                                               "1043864434",
                                               new DateTime(2022, 07, 27, 19, 58, 0),
                                               OperationTypeEnum.Income,
                                               164.72m);
var test3 = await APIProvider.GetReceiptByQRUrl("https://proxy.imgsmail.ru/?e=1660982567&email=s.polonski%40mail.ru&flags=0&h=ICsLBbba3cT7wLKJoEMVVg&is_https=1&url173=b25saW5lLnNiaXMucnUvdmVkL3NlcnZpY2UvP2lkPTAmbWV0aG9kPUJhcmNvZGUuR2VuZXJhdGVUb1JwYyZwcm90b2NvbD02JnBhcmFtcz1leUpRWVhKaGJYTWlPbnNpWmlJNk1Dd2laQ0k2V3lJak9UazVPVGs1SWl3aWRISmhibk53WVhKbGJuUWlMREpkTENKeklqcGJleUp1SWpvaVJISmhkME52Ykc5eUlpd2lkQ0k2SXRDaDBZTFJnTkMlMkIwTHJRc0NKOUxIc2liaUk2SWtKaFkydG5jbTkxYm1SRGIyeHZjaUlzSW5RaU9pTFFvZEdDMFlEUXZ0QzYwTEFpZlN4N0ltNGlPaUpQZFhSd2RYUlVlWEJsSWl3aWRDSTZJdENuMExqUmdkQzcwTDRnMFliUXRkQzcwTDdRdFNKOVhTd2lYM1I1Y0dVaU9pSnlaV052Y21RaWZTd2lWSGx3WlNJNklsRlNJaXdpVm1Gc2RXVWlPaUowUFRJd01qSXdPREEyVkRFM05ESXdNQ1p6UFRrME9DNHdNQ1ptYmowNU1qZzNORFF3TXpBd09UazNPVGN4Sm1rOU56RTFOVGdtWm5BOU9EZzJOREF5TnpZNEptNDlNU0o5");
//var test32 = await APIProvider.GetReceiptByQRUrl("https://af12.mail.ru/cgi-bin/readmsg?id=16586760310840212176;0;1;1&mode=attachment&email=s.polonski@mail.ru&ct=image%2fpng&cn=&cte=binary");
//var test4 = await APIProvider.GetReceiptByQRImage(new Bitmap("D:\\DownloadsYandex\\QRtest.png"));

DatabaseProvider.SaveReceipt(test1);
DatabaseProvider.SaveReceipt(test2);

