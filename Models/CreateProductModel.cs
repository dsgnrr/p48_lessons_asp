using System;

namespace p48_lessons_asp.Models;

public class CreateProductModel
{
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public string? Description { get; set; }
    // Для обробки файлів використовується спеціальний інтерфейс
    public IFormFile? productImage{get;set;}

    public Product Product { get => new Product(Name, Price, Description); }
}
