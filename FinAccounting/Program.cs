// See https://aka.ms/new-console-template for more information
using FinAccounting.Database;
/*using MailKit.Net.Imap;
using MailKit;
using MailKit.Search;

MailClient mailClient = new(new MailSettings(), DateTime.Parse("2022-01-01"));

var a = mailClient.GetReceiptListFromEMail();*/

Console.WriteLine("Hello, World!");

Product product = new Product() { title = "Test" };
using (DatabaseContext db = new DatabaseContext())
{
    var a = DatabaseProvider.GetProductByTitle("Test");
    db.product.Add(product);
    db.SaveChanges();
}
Console.WriteLine($"{product.product_id} - {product.title}");