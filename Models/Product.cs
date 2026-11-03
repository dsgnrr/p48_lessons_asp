namespace p48_lessons_asp.Models;

public class Product
{
    private static int _id;

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = "no-set";
    public double Price { get; set; }

    public Product() : this(_id)
    {
        Id = _id++;
    }
    public Product(int id)
    {
        Id = id;
        Name = $"Name-{Id}";
        Description = "Lorem ipsum — це класичний умовний беззмістовний текст («риба»), який використовується в типографіці, дизайні та верстці для заповнення простору макета перед тим, як з'явиться справжній контент";
        Price = Math.Round(Random.Shared.NextDouble() * 900 + 100, 2);
    }
    public Product(string name, double price, string? description)
    {
        Id = (int)Random.Shared.NextInt64(0, 100);
        Name = name;
        Price = price;
        /*
        int? x = null;
        int y = x ?? 10; // if x is null: y = 10 if x haValue: y = x
        */
        Description = description ?? "no-set";
    }

    public override string ToString()
    {
        return @$"
        Product:
        Id: {Id},
        Name: {Name},
        Price: {Price},
        Description: {Description}
        ";
    }
}