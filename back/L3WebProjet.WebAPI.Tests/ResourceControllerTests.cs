namespace L3WebProjet.WebAPI.Tests;

using Xunit;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using L3WebProjet.WebAPI.Controllers;
using L3WebProjet.Business.Interfaces;

public class ResourceControllerTests
{
    [Fact]
    public async Task Calculate_ReturnsOk_WithTotalMoneyGenerated()
    {
        var mockService = new Mock<IResourceService>();
        var storeId = Guid.NewGuid();

        mockService.Setup(s => s.CalculateMoneyAsync(storeId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(42);

        var controller = new ResourceController(mockService.Object);
        var result = await controller.Calculate(storeId, CancellationToken.None);
        var okResult = Assert.IsType<OkObjectResult>(result);

        var resultObject = okResult.Value;

        var totalProperty = resultObject?.GetType().GetProperty("money");
        Assert.NotNull(totalProperty);

        var rawValue = totalProperty!.GetValue(resultObject);
        Assert.NotNull(rawValue); 

        var totalValue = (int)(rawValue!);

        Assert.Equal(42, totalValue);
    }



    [Fact]
    public async Task Calculate_ReturnsBadRequest_WhenServiceReturnsNegative()
    {
        var mockService = new Mock<IResourceService>();
        var storeId = Guid.NewGuid();

        mockService.Setup(s => s.CalculateMoneyAsync(storeId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(-1);

        var controller = new ResourceController(mockService.Object);
        var result = await controller.Calculate(storeId, CancellationToken.None);
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Store or resource not found", badRequest.Value);
    }

}
