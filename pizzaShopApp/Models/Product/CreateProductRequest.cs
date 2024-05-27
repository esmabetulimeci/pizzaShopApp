﻿using Application.Operations.Product.Commands;
using Domain.Model;

namespace pizzaShopApi.Models.Product
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<Ingredients> Ingredients { get; set; }
        public double Quantity { get; set; }

        public AddProductCommand ToCommand()
        {
            return new AddProductCommand
            (
             Name, Description, Price, Ingredients, Quantity
            );
        }
    }
}
