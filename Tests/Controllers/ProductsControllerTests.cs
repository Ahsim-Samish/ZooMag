using Microsoft.AspNetCore.Mvc;
using ProductStoreApi.Controllers;
using ProductStoreApi.Data;
using ProductStoreApi.Models;
using System.Threading.Tasks;
using Xunit;

namespace ProductStoreApi.Tests.Controllers;

public class ProductsControllerTests
{
    private readonly ProductsController _controller;
    private readonly ApplicationDbContext _context;

    public ProductsControllerTests()
    {
        _context = TestHelper.GetInMemoryDbContext();
        _controller = new ProductsController(_context);
    }

    [Fact]
    public async Task GetProducts_ShouldReturnAllProducts()
    {
        // Act
        var result = await _controller.GetProducts();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Product>>>(result);
        var returnValue = Assert.IsType<List<Product>>(actionResult.Value);
        Assert.Single(returnValue); // из SeedData
    }

    [Fact]
    public async Task GetProduct_ShouldReturnProduct_WhenExists()
    {
        // Act
        var result = await _controller.GetProduct(1);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Product>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var product = Assert.IsType<Product>(okResult.Value);

        Assert.Equal("Молоко 1л", product.Name);
    }

    [Fact]
    public async Task GetProduct_ShouldReturnNotFound_WhenNotExists()
    {
        // Act
        var result = await _controller.GetProduct(999);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostProduct_ShouldCreateProduct()
    {
        // Arrange
        var newProduct = new Product
        {
            Name = "Яблоки 1кг",
            Description = "Сочные",
            Price = 129.90m,
            Stock = 50
        };

        // Act
        var result = await _controller.PostProduct(newProduct);

        // Assert
        var createdAtAction = Assert.IsType<CreatedAtActionResult>(result.Result);
        var product = Assert.IsType<Product>(createdAtAction.Value);
        Assert.Equal("Яблоки 1кг", product.Name);
        Assert.True(product.Id > 1);
    }
}