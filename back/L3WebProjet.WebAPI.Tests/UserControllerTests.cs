namespace L3WebProjet.WebAPI.Tests;

using Xunit;
using Moq;
using L3WebProjet.WebAPI.Controllers;
using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.Request;
using L3WebProjet.Common.DTO;
using Microsoft.AspNetCore.Mvc;

public class UserControllerTests
{
    [Fact]
    public async Task CreateWithStore_Returns_Ok_With_User()
    {
        // On mock pour simuler les données
        var mockService = new Mock<IUserService>();
        var request = new UserWithStoreCreateRequest
        {
            Pseudo = "Tom",
            StoreName = "VidéoTop"
        };

        var expectedUser = new UserDto
        {
            Id = Guid.NewGuid(),
            Pseudo = "Tom"
        };

        mockService.Setup(s => s.CreateUserWithStoreAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedUser);

        var controller = new UserController(mockService.Object);

        var result = await controller.CreateWithStore(request, CancellationToken.None);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var user = Assert.IsType<UserDto>(createdResult.Value);
        Assert.Equal("Tom", user.Pseudo);
    }
}