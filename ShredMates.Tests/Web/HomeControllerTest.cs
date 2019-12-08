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
        private readonly ShoppingCart shoppingCart;

        public HomeControllerTest()
        {
            this.shoppingCart = TestStartup.CreateShoppingCart();
        }

        [Fact]
        public async Task Index_ShouldReturn_HomeViewModel()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>().Object;
            var mockHomeController = new Mock<HomeController>(mockProductService, shoppingCart).Object;
            var mockHomeViewModel = new Mock<ProductListingViewModel>().Object;

            mockHomeViewModel.Products = new List<ProductListingServiceModel>()
            {
                new ProductListingServiceModel { Id = 1, Title = "A", Price = 1},
                new ProductListingServiceModel { Id = 2, Title = "B", Price = 1},
                new ProductListingServiceModel { Id = 3, Title = "C", Price = 1}
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
            var result = await mockHomeController.Index(); // returns null, stops at session set string

            //Assert
            Assert.Null(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            //var model = Assert.IsType<HomeViewModel>(viewResult.Model);
        }

        [Fact]
        public async Task Search_ShouldReturn_SearchViewModel()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>().Object;
            var shoppingCart = new Mock<ShoppingCart>().Object;
            var homeController = new HomeController(mockProductService, shoppingCart);
            var mockHomeViewModel = new Mock<ProductListingViewModel>().Object;

            // Act
            var result = await homeController.Search(mockHomeViewModel);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<ProductListingViewModel>(viewResult.Model);
        }
    }
}
