namespace p48_lessons_asp.Data.Entity;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } 
    public double Price{get;set;}
    public DateTime CreatedAt{get;set;} = DateTime.Now;
    public DateTime? DeletedAt{get;set;}    
}