using Moq;
using Microsoft.AspNetCore.Mvc;
using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using L3WebProjet.WebAPI.Controllers;

namespace L3WebProjet.WebAPI.Tests
{
    public class WarehouseControllerTests
    {
        [Fact]
        public async Task GetByStoreId_ReturnsOk_WhenWarehouseExists()
        {
            var mockService = new Mock<IWarehouseService>();
            var storeId = Guid.NewGuid();
            var warehouse = new WarehouseDto { Id = Guid.NewGuid(), StoreId = storeId, Level = 1 };

            mockService.Setup(s => s.GetByStoreIdAsync(storeId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(warehouse);

            var controller = new WarehouseController(mockService.Object);

            var result = await controller.GetByStoreId(storeId, CancellationToken.None);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedWarehouse = Assert.IsType<WarehouseDto>(okResult.Value);
            Assert.Equal(storeId, returnedWarehouse.StoreId);
        }

        [Fact]
        public async Task GetByStoreId_ReturnsNotFound_WhenWarehouseDoesNotExist()
        {
            var mockService = new Mock<IWarehouseService>();
            var storeId = Guid.NewGuid();

            mockService.Setup(s => s.GetByStoreIdAsync(storeId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((WarehouseDto?)null);

            var controller = new WarehouseController(mockService.Object);

            var result = await controller.GetByStoreId(storeId, CancellationToken.None);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Entrepôt non trouvé", notFoundResult.Value);
        }

        [Fact]
        public async Task Upgrade_ReturnsOk_WhenUpgradeSuccessful()
        {
            var mockService = new Mock<IWarehouseService>();
            var storeId = Guid.NewGuid();

            mockService.Setup(s => s.UpgradeWarehouseAsync(storeId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

            var controller = new WarehouseController(mockService.Object);

            var result = await controller.Upgrade(storeId, CancellationToken.None);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Entrepôt amélioré", okResult.Value);
        }

        [Fact]
        public async Task Upgrade_ReturnsBadRequest_WhenUpgradeFails()
        {
            var mockService = new Mock<IWarehouseService>();
            var storeId = Guid.NewGuid();

            mockService.Setup(s => s.UpgradeWarehouseAsync(storeId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(false);

            var controller = new WarehouseController(mockService.Object);

            var result = await controller.Upgrade(storeId, CancellationToken.None);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Entrepôt introuvable ou niveau maximum atteint ou argent insuffisant", badRequestResult.Value);
        }
    }
}
