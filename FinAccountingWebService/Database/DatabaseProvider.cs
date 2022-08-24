using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FinAccountingWebService.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace FinAccountingWebService.Database
{
    public static class DatabaseProvider
    {
        public static void SaveReceipt(APIReceipt.Receipt receipt)
        {
            Tables.Receipt receiptEntity = new()
            {
                Datetime = receipt.DateTime.ToUniversalTime(),
                Shop = new() { Title = receipt.Organization },
                Total = receipt.Total
            };

            foreach (var item in receipt.Items)
            {
                receiptEntity.ReceiptPositions.Add(new()
                {
                    Product = new() { Title = item.Name },
                    Count = item.Quantity,
                    Price = item.Price
                });
            }
            try
            {
                using (DatabaseContext db = new())
                {
                    db.Shop.AddIfNotExists(receiptEntity.Shop);
                    foreach (var position in receiptEntity.ReceiptPositions)
                    {
                        db.Product.AddIfNotExists(position.Product);
                        db.ReceiptPosition.Add(position);
                    }
                    db.Receipt.Add(receiptEntity);
                    db.SaveChanges();
                }
            } catch (DbUpdateException ex)
            {
                Console.WriteLine($"Ошибка сохренения чека: {ex.Message}");
            }
        }

        public static bool CheckConnection()
        {
            using DatabaseContext db = new();
            return db.Database.CanConnect();
        }

        #region Группа методов Get
        public static void GetReceiptList()
        {

        }
        public static void GetReceiptByDate()
        {

        }
        public static void GetReceiptByPeriod()
        {

        }

        public static void GetReceiptPosition()
        {

        }

        public static Dictionary<string, long> GetProductIdsByTitleList(List<string> productList)
        {
            Dictionary<string, long> products;
            using (DatabaseContext db = new DatabaseContext())
            {
                products = db.Product.Where(p => productList.Contains(p.Title)).ToDictionary(p => p.Title, p => p.ProductId);
            }
            return products;
        }
        /// <summary>
        /// Поиск товара в базе данных по названию. Регистронезависимый. TODO: уточнить про регистр
        /// </summary>
        /// <param name="title">Название товара.</param>
        /// <returns>Объект товара.</returns>
        public static Product? GetProductByTitle(string title)
        {
            List<Product> products;
            using (DatabaseContext db = new DatabaseContext())
            {
                products = db.Product.Where(p => p.Title == title).ToList();
            }
            return products?[0] ?? null;
        }

        public static long GetShopId(string title)
        {
            return -1;
        }
        #endregion

        #region Группа методов Add

        /// <summary>
        /// Добавление чека в базу данных.
        /// </summary>
        /// <param name="receiptPositions">Список позиций по чеку.</param>
        /// <param name="datetime">Время выполнения операции</param>
        /// <param name="total">Итог по чеку</param>
        /// <returns>ID товара в базе данных</returns>
        public static long AddReceipt(List<ReceiptPosition> receiptPositions, DateTime datetime, decimal total)
        {
            Tables.Receipt newReceipt = new Tables.Receipt() { Datetime = datetime, Total = total };
            using (DatabaseContext db = new DatabaseContext())
            {
                db.Receipt.Add(newReceipt);
                db.SaveChanges();
            }

            AddReceiptPositionRange(receiptPositions, newReceipt.ReceiptId);
            return newReceipt.ReceiptId;
        }

        public static void AddReceiptPositionRange(List<ReceiptPosition> receiptPositions, long receipt_id)
        {
            foreach (var receiptPosition in receiptPositions)
            {
                receiptPosition.ReceiptId = receipt_id;
            }

            using (DatabaseContext db = new DatabaseContext())
            {
                db.ReceiptPosition.AddRange(receiptPositions);
                db.SaveChanges();
            }
        }
        /*public static void AddReceiptPosition()
        {
            ReceiptPosition newReceiptPosition = new ReceiptPosition() { };
            AddReceiptPosition(newReceiptPosition);
        }
        public static void AddReceiptPosition(ReceiptPosition receiptPosition)
        {

        }*/

        /// <summary>
        /// Добавление товара в базу данных.
        /// При добавлении происходит проверка на наличие уже имеющегося товара. В случае, если товар был найден, то возращается его ID без добавления нового.
        /// </summary>
        /// <param name="title">Название товара.</param>
        /// <returns>ID товара в базе данных</returns>
        public static long AddProduct(string title)
        {
            Product newProduct = new Product() { Title = title };
            return AddProduct(newProduct);
        }

        /// <summary>
        /// Добавление товара в базу данных. 
        /// При добавлении происходит проверка на наличие уже имеющегося товара. В случае, если товар был найден, то возращается его ID без добавления нового.
        /// </summary>
        /// <param name="product">Объект товара.</param>
        /// <returns>ID товара в базе данных</returns>
        public static long AddProduct(Product product)
        {
            Product? existsProduct = GetProductByTitle(product.Title);
            if (existsProduct != null)
            {
                return existsProduct.ProductId;
            }

            using (DatabaseContext db = new DatabaseContext())
            {
                db.Product.Add(product);
                db.SaveChanges();
            }
            return product.ProductId;
        }

        public static void AddShop()
        {

        }
        #endregion
    }
}
