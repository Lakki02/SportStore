using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SportStore.Controllers;
using SportStore.Models;
using SportSrore.Models;
using Xunit;
using SportSrore.Models.ViewModels;

namespace SportSore.Test
{
    public class ProductControllerTest
    {
        [Fact]
        public void Can_Paginat()
        {
            //Организация
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            }).AsQueryable<Product>);

            ProductController controller = new ProductController(mock.Object);
            
            controller.PageSize = 3;
            //Действие
            // IEnumerable<Product> result = 
            //   controller.list(2).ViewData.Model as IEnumerable<Product>;
            ProductsListViewModel result = 
                controller.list(2).ViewData.Model as ProductsListViewModel;

            //Утверждение
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_view_model()
        {
            //Организация
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            }).AsQueryable<Product>());

            ProductController controller = 
                new ProductController(mock.Object) { PageSize = 3 };

            //Действие
            ProductsListViewModel result = controller.list(2).ViewData.Model as ProductsListViewModel;

            //Утверждение
            PagingInfo pageInfo =result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(2, pageInfo.TotalPages);
            Assert.Equal(5, pageInfo.TotalItem);
            
        }
    }
}
