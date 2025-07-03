namespace L3WebProjet.WebAPI.Tests;

using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using L3WebProjet.WebAPI.Controllers;
using L3WebProjet.Business.Interfaces;

public class SectionControllerTests
{
    [Fact]
    public async Task UpgradeSection_ReturnsOk_WhenUpgradeSuccessful()
    {
        var mockService = new Mock<ISectionService>();
        var sectionId = Guid.NewGuid();

        mockService.Setup(s => s.UpgradeSectionAsync(sectionId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var controller = new SectionController(mockService.Object);

        var result = await controller.UpgradeSection(sectionId, CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Section upgraded", okResult.Value);
    }

    [Fact]
    public async Task UpgradeSection_ReturnsBadRequest_WhenUpgradeFails()
    {
        var mockService = new Mock<ISectionService>();
        var sectionId = Guid.NewGuid();

        mockService.Setup(s => s.UpgradeSectionAsync(sectionId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var controller = new SectionController(mockService.Object);

        var result = await controller.UpgradeSection(sectionId, CancellationToken.None);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Not enough money or section not found", badRequest.Value);
    }
}