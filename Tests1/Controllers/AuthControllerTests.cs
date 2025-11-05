using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using PetStoreApi.Controllers;
using PetStoreApi.Data;
using PetStoreApi.DTOs;
using PetStoreApi.Models;
using System.Threading.Tasks;
using Xunit;

namespace PetStoreApi.Tests.Controllers;

public class AuthControllerTests
{
    private readonly AuthController _controller;
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthControllerTests()
    {
        _context = TestHelper.GetInMemoryDbContext();

        var config = new Dictionary<string, string>
        {
            { "JwtSettings:SecretKey", "your-super-secret-jwt-key-here-1234567890" },
            { "JwtSettings:Issuer", "TestApi" },
            { "JwtSettings:Audience", "TestClients" },
            { "JwtSettings:ExpireMinutes", "60" }
        };

        _configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
        _controller = new AuthController(_context, _configuration);
    }

    [Fact]
    public async Task Register_ShouldCreateUser_AndReturnToken()
    {
        // Arrange
        var registerDto = new RegisterDto
        {
            Username = "newuser",
            Email = "new@example.com",
            Password = "123456"
        };

        // Act
        var result = await _controller.Register(registerDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var response = Assert.IsType<AuthResponseDto>(okResult.Value);
        Assert.NotEmpty(response.Token);
        Assert.Equal("newuser", response.Username);
    }

    [Fact]
    public async Task Login_ShouldReturnToken_WhenCredentialsValid()
    {
        // Arrange
        var loginDto = new LoginDto
        {
            Email = "test@example.com",
            Password = "123456"
        };

        // Act
        var result = await _controller.Login(loginDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var response = Assert.IsType<AuthResponseDto>(okResult.Value);
        Assert.NotEmpty(response.Token);
    }

    [Fact]
    public async Task Login_ShouldReturnUnauthorized_WhenPasswordInvalid()
    {
        // Arrange
        var loginDto = new LoginDto
        {
            Email = "test@example.com",
            Password = "wrongpassword"
        };

        // Act
        var result = await _controller.Login(loginDto);

        // Assert
        Assert.IsType<UnauthorizedObjectResult>(result.Result);
    }
}