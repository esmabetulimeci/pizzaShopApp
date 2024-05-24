using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public virtual List<ProductAggregate> Products { get; set; }

        public OrderAggregate()
        {
            // only db
        }

        private OrderAggregate(string orderNumber, double totalAmount, double discountAmount, DateTime orderDate, string customerName, List<ProductAggregate> products)
        {
            OrderNumber = GenerateOrderNumber();
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            OrderDate = DateTime.Now;
            CustomerName = customerName;
            Products = products;
        }

        public static OrderAggregate Create(string customerName, List<ProductAggregate> products)
        {

            string orderNumber = GenerateOrderNumber();
            double totalAmount = 0;
            foreach (var product in products)
            {
                totalAmount += product.Price * product.Quantity;
            }

            return new OrderAggregate(orderNumber, totalAmount, 0, DateTime.Now, customerName, products);
        }



        private static string GenerateOrderNumber()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void Update(string newCustomerName, List<ProductAggregate> newProducts)
        {

            CustomerName = newCustomerName;
            Products = newProducts;
            TotalAmount = newProducts.Sum(p => p.Price);
            OrderDate = DateTime.Now;

            // _dbContext.SaveChanges();
        }





    }




}