using Microsoft.EntityFrameworkCore;
using ProductStoreApi.Data;
using ProductStoreApi.Models;

namespace ProductStoreApi.Tests;

public static class TestHelper
{
    public static ApplicationDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);
        SeedData(context);
        return context;
    }

    private static void SeedData(ApplicationDbContext context)
    {
        var user = new User
        {
            Id = 1,
            Username = "testuser",
            Email = "test@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
        };

        var product = new Product
        {
            Id = 1,
            Name = "Молоко 1л",
            Description = "Пастеризованное",
            Price = 89.90m,
            Stock = 100
        };

        context.Users.Add(user);
        context.Products.Add(product);
        context.SaveChanges();
    }
}