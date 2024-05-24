using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Model
{
    public class OrderAggregate
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public virtual List<ProductAggregate> Products { get; set; }

        public OrderAggregate()
        {
            // only db
        }

        private OrderAggregate(string orderNumber, double totalAmount, double discountAmount, DateTime orderDate, string customerName, string customerAddress, List<ProductAggregate> products)
        {
            OrderNumber = orderNumber;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            OrderDate = orderDate;
            CustomerName = customerName;
            CustomerAddress = customerAddress;
            Products = products;
        }

        public static OrderAggregate Create(string customerName, string customerAddress, List<ProductAggregate> products)
        {
            string orderNumber = GenerateOrderNumber();
            double totalAmount = products.Sum(product => product.Price * product.Quantity);

            return new OrderAggregate(orderNumber, totalAmount, 0, DateTime.Now, customerName, customerAddress, products);
        }

        public void Update(string customerName, string customerAddress, List<ProductAggregate> products)
        {
            CustomerName = customerName;
            CustomerAddress = customerAddress;
            foreach (var product in products)
            {
                
                var existingProduct = Products.FirstOrDefault(p => p.Id == product.Id);
                if (existingProduct != null)
                {
                    existingProduct.Quantity = product.Quantity;
                }
                else
                {
                    Products.Add(product);
                }
            }
        }

        private static string GenerateOrderNumber()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
