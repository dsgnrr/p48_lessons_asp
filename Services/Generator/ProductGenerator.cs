using System;
using p48_lessons_asp.Models;

namespace p48_lessons_asp.Services.Generator;

public class ProductGenerator : IGenerator<Product>
{
    public List<Product> Generate(int count)
    {
        var products = new List<Product>();
        for (int i = 0; i < count; i++)
        {
            products.Add(new Product(i));
        }
        return products;
    }
}
