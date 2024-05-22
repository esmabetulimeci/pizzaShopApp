using Application.Product.Commands;
using Domain.Model;

namespace pizzaShopApi.Models.Product
{
    public class UpdateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<Ingredients> Ingredients { get; set; }
        public double Quantity { get; set; }

        //public UpdateProductCommand ToCommand()
        //{
        //    return new UpdateProductCommand
        //    (
        //       Name, Description, Price, Ingredients, Quantity
        //    );
        //}
    }
}
