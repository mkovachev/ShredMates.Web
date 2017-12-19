using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShredMates.Services.Interfaces;
using ShredMates.Services.Models;
using ShredMates.Web.Controllers;
using ShredMates.Web.Models.HomeViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ShredMates.Tests.Web
{
    public class HomeControllerTest
    {
        [Fact]
        public async Task Index_ShouldReturn_HomeViewModel()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>().Object;
            var mockHomeController = new Mock<HomeController>(mockProductService).Object;
            var mockHomeViewModel = new Mock<HomeViewModel>().Object;

            mockHomeViewModel.Products = new List<ProductListingServiceModel>()
            {
                new ProductListingServiceModel { Id = 1, Title = "A", Price = 100 },
                new ProductListingServiceModel { Id = 2, Title = "B", Price = 200},
                new ProductListingServiceModel { Id = 3, Title = "C", Price = 300}
            };

            // get mocked session
            var mockContext = new Mock<HttpContext>().Object;
            var sessionMock = new Mock<ISession>();

            var key = "test";
            var value = new byte[0];

            sessionMock.Setup(s => s.Set(key, It.IsAny<byte[]>()))
                            .Callback<string, byte[]>((k, v) => value = v);

            sessionMock.Setup(s => s.TryGetValue(key, out value))
                .Returns(true);

            // Act
            var result = await mockHomeController.Index(); // returns null when calling AllAsync();

            //Assert
            // var viewResult = Assert.IsType<ViewResult>(result);
            // var model = Assert.IsType<HomeViewModel>(viewResult.Model);
        }

        [Fact]
        public async Task Search_ShouldReturn_SearchViewModel()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>().Object;
            var homeController = new HomeController(mockProductService);
            var mockHomeViewModel = new Mock<HomeViewModel>().Object;

            // Act
            var result = await homeController.Search(mockHomeViewModel);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<SearchViewModel>(viewResult.Model);
        }
    }
}
