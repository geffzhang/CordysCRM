using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using CordysCRM.CRM.Services;
using CordysCRM.CRM.Repositories;
using CordysCRM.CRM.Domain;
using CordysCRM.CRM.DTOs;

namespace CordysCRM.Tests;

public class CustomerServiceTests
{
    private readonly Mock<ICustomerRepository> _mockRepository;
    private readonly Mock<ILogger<CustomerService>> _mockLogger;
    private readonly CustomerService _service;

    public CustomerServiceTests()
    {
        _mockRepository = new Mock<ICustomerRepository>();
        _mockLogger = new Mock<ILogger<CustomerService>>();
        _service = new CustomerService(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCustomer_WhenExists()
    {
        // Arrange
        var customerId = "test-id";
        var expectedCustomer = new Customer { Id = customerId, Name = "Test Customer" };
        _mockRepository.Setup(r => r.GetByIdAsync(customerId, default))
            .ReturnsAsync(expectedCustomer);

        // Act
        var result = await _service.GetByIdAsync(customerId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customerId, result.Id);
        Assert.Equal("Test Customer", result.Name);
    }

    [Fact]
    public async Task CreateAsync_CreatesCustomer_WithCorrectData()
    {
        // Arrange
        var dto = new CustomerCreateDto { Name = "New Customer", InSharedPool = false };
        var userId = "user-1";
        var orgId = "org-1";

        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Customer>(), default))
            .ReturnsAsync((Customer c, CancellationToken ct) => c);

        // Act
        var result = await _service.CreateAsync(dto, userId, orgId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New Customer", result.Name);
        Assert.Equal(orgId, result.OrganizationId);
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<Customer>(), default), Times.Once);
    }
}
