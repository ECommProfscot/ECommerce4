using System;
using Xunit;
using ECommerce.Api.Products.Providers;
using ECommerce.Api.Products.Db;
using Microsoft.EntityFrameworkCore;
using ECommerce.Api.Products.Profiles;
using AutoMapper;
using System.Linq;

namespace ECommerc.Api.Products.Test
{
    public class ProductsServiceTest
    {
        [Fact]
        public async void GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts)).Options;
            var dbContext = new ProductsDbContext( options  );

            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);
            var result = await productsProvider.GetProductsAsync();

            Assert.True(result.IsSuccess);
            Assert.True(result.Products.Any());
            Assert.Null(result.ErrorMessage);
        }

        [Fact]
        public async void GetProductsReturnsProductById()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductsReturnsProductById)).Options;
            var dbContext = new ProductsDbContext(options);

            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);
            var result = await productsProvider.GetProductAsync(4);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Product);
            Assert.Null(result.ErrorMessage);
            Assert.True(result.Product.Id == 4);
        }


        [Fact]
        public async void GetProductsReturnsProductByWrongId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductsReturnsProductByWrongId)).Options;
            var dbContext = new ProductsDbContext(options);

            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);
            var result = await productsProvider.GetProductAsync(-4);

            Assert.False(result.IsSuccess);
            Assert.Null(result.Product);
            Assert.NotNull(result.ErrorMessage);
        }

        private void CreateProducts(ProductsDbContext dbContext)
        {
            foreach (var product in dbContext.Products)
            {
                dbContext.Products.Remove(product);
            }
            dbContext.SaveChanges();

            for (int i = 3; i <= 10; i++)
            {
                dbContext.Products.Add(new Product() { Id = i, Inventory = i * 10, Name = "Product" + i, price = (decimal)(i * 1.5) });               
            }
            dbContext.SaveChanges();
        }
    }
}
