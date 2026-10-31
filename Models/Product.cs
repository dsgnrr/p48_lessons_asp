namespace p48_lessons_asp.Models;

public class Product
{
    private static int _id;
    public Product(): this(_id)
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
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    

}