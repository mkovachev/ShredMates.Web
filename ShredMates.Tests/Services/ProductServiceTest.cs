﻿using AutoMapper;
using FluentAssertions;
using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Implementations;
using ShredMates.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ShredMates.Tests.Services
{
    public class ProductServiceTest
    {
        private readonly IMapper mapper;
        private readonly ShredMatesDbContext db;

        public ProductServiceTest()
        {
            this.db = TestStartup.CreateDatabase();
            this.mapper = TestStartup.CreateMapper();
        }

        [Fact]
        public async Task AllAsync_ShouldReturn_AllProducts()
        {
            // Arrange      
            var productService = new ProductService(db, mapper);

            // Act
            var result = await productService.AllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.IsType<List<ProductListingServiceModel>>(result);
        }

        [Fact]
        public async Task FindAsync_ShouldReturn_ProductById()
        {
            // Arrange
            var productService = new ProductService(db, mapper);

            // Act
            var result = await productService.ByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            result.Should()
                .Match<Product>(p => p.Id == 1
                                    && p.Title == "TestProduct1");
        }
    }
}
