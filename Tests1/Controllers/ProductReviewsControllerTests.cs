using Microsoft.AspNetCore.Mvc;
using PetStoreApi.Controllers;
using PetStoreApi.Data;
using PetStoreApi.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PetStoreApi.Tests.Controllers;

public class ProductReviewsControllerTests
{
    private readonly ProductReviewsController _controller;
    private readonly ApplicationDbContext _context;

    public ProductReviewsControllerTests()
    {
        _context = TestHelper.GetInMemoryDbContext();
        _controller = new ProductReviewsController(_context);
    }

    [Fact]
    public async Task PostReview_ShouldCreateReview_WhenValid()
    {
        // Arrange
        var review = new ProductReview
        {
            Rating = 5,
            Comment = "Отличный товар!",
            ProductId = 1,
            UserId = 1
        };

        // Act
        var result = await _controller.PostReview(review);

        // Assert
        var createdAtAction = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedReview = Assert.IsType<ProductReview>(createdAtAction.Value);
        Assert.Equal(5, returnedReview.Rating);
        Assert.NotNull(returnedReview.CreatedAt);
    }

    [Fact]
    public async Task PostReview_ShouldReturnBadRequest_WhenProductNotFound()
    {
        // Arrange
        var review = new ProductReview
        {
            Rating = 5,
            Comment = "Хорошо",
            ProductId = 999,
            UserId = 1
        };

        // Act
        var result = await _controller.PostReview(review);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Product not found.", badRequest.Value);
    }
}