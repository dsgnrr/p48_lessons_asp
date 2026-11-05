using System;
using Microsoft.EntityFrameworkCore;
using p48_lessons_asp.Data.Entity;

namespace p48_lessons_asp.Data;

public class DataContext:DbContext
{
    public DataContext(DbContextOptions options):base(options){}

    public DbSet<Product> Products{get;set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .ToTable(t=>t.HasCheckConstraint("CK_Product_Price_Positive", "Price > 0"));
    }
}
