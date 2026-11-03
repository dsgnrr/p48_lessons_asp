using System;
using Microsoft.EntityFrameworkCore;

namespace p48_lessons_asp.Data;

public class DataContext:DbContext
{
    public DataContext(DbContextOptions options):base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}
