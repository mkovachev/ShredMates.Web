﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using ShredMates.Services.Interfaces;
using ShredMates.Web.Controllers;
using System.Threading.Tasks;
using Xunit;

namespace ShredMates.Tests.Web
{
    public class AdminCategoryControllerTest
    {
        [Fact]
        public async Task Index_ShouldReturn_CategoryViewModel_WithAllProducts()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var CategoryController = new CategoryController(mockCategoryService.Object);
            var id = 1; // category

            // Act
            var result = await CategoryController.Index(id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<CategoryViewModel>(viewResult.Model);
        }
    }
}
