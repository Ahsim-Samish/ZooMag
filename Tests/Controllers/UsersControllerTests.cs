using Microsoft.AspNetCore.Mvc;
using ProductStoreApi.Controllers;
using ProductStoreApi.Models;
using ProductStoreApi.Data;
using System.Threading.Tasks;
using Xunit;
using ProductStoreApi.Tests;

namespace ProductStoreApi.Tests.Controllers;

public class UsersControllerTests
{
    private readonly UsersController _controller;
    private readonly ApplicationDbContext _context;

    public UsersControllerTests()
    {
        _context = TestHelper.GetInMemoryDbContext();
        _controller = new UsersController(_context);
    }

    [Fact]
    public async Task GetUsers_ShouldReturnAllUsers()
    {
        // Act
        var result = await _controller.GetUsers();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<User>>>(result);
        var users = Assert.IsType<List<User>>(actionResult.Value);
        Assert.Single(users);
    }

    [Fact]
    public async Task DeleteUser_ShouldRemoveUser_WhenExists()
    {
        // Act
        var result = await _controller.DeleteUser(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
        Assert.Empty(_context.Users);
    }
}