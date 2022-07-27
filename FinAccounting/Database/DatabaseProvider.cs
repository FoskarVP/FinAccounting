using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinAccounting.Database
{
    public static class DatabaseProvider
    {
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

        public static void GetProductList()
        {

        }
        /// <summary>
        /// Поиск товара в базе данных по названию. Регистронезависимый.
        /// </summary>
        /// <param name="title">Название товара.</param>
        /// <returns>Объект товара.</returns>
        public static Product? GetProductByTitle(string title)
        {
            List<Product> products;
            using (DatabaseContext db = new DatabaseContext())
            {
                products = db.product.Where(p => p.title== title).ToList();
            }
            return products?[0] ?? null;
        }

        public static void GetShop()
        {

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
        public static long AddReceipt(List<ReceiptPosition> receiptPositions, DateTime datetime, double total)
        {
            Receipt newReceipt = new Receipt() { datetime = datetime, total = total };
            using (DatabaseContext db = new DatabaseContext())
            {
                db.receipt.Add(newReceipt);
                db.SaveChanges();
            }

            AddReceiptPositionRange(receiptPositions, newReceipt.receipt_id);
            return newReceipt.receipt_id;
        }

        public static void AddReceiptPositionRange(List<ReceiptPosition> receiptPositions, long receipt_id)
        {
            foreach (var receiptPosition in receiptPositions)
            {
                receiptPosition.receipt_id = receipt_id;
            }

            using (DatabaseContext db = new DatabaseContext())
            {
                db.receipt_position.AddRange(receiptPositions);
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
            Product newProduct = new Product() { title = title };
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
            Product? existsProduct = GetProductByTitle(product.title);
            if (existsProduct != null)
            {
                return existsProduct.product_id;
            }

            using (DatabaseContext db = new DatabaseContext())
            {
                db.product.Add(product);
                db.SaveChanges();
            }
            return product.product_id;
        }

        public static void AddShop()
        {

        }
        #endregion
    }
}
