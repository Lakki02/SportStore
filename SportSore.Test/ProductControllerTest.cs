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
using Microsoft.AspNetCore.Mvc;

namespace SportSore.Test
{
    public class ProductControllerTest
    {
        [Fact]
        public void Can_Paginate()
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
                controller.list(null, 2).ViewData.Model as ProductsListViewModel;

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
            ProductsListViewModel result = controller.list(null,2).ViewData.Model as ProductsListViewModel;

            //Утверждение
            PagingInfo pageInfo =result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(2, pageInfo.TotalPages);
            Assert.Equal(5, pageInfo.TotalItem);
            
        }

        [Fact]
        public void Can_Filter_Product()
        {
            //Организациия
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(p => p.Products).Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
            }).AsQueryable<Product>());

            //Организация - создание контроллера и установка размера страницы в три элемента
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Действие
            Product[] result = (controller.list("Cat2", 1).ViewData.Model 
                    as ProductsListViewModel)
                .Products.ToArray();

            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P2" && result[0].Category =="Cat2");
            Assert.True(result[1].Name == "P4" && result[1].Category == "Cat2");
        }

        [Fact]
        public void Generate_Category_Specific_Product_count()
        {
            //Организация
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
            }).AsQueryable<Product>());

            ProductController target = new ProductController(mock.Object);

            target.PageSize = 3;

            Func<ViewResult, ProductsListViewModel> GetModel = result =>
                result?.ViewData?.Model as ProductsListViewModel;

            //Действие

            int? res1 = GetModel(target.list("Cat1"))?.PagingInfo.TotalItem;
            int? res2 = GetModel(target.list("Cat2"))?.PagingInfo.TotalItem;
            int? res3 = GetModel(target.list("Cat3"))?.PagingInfo.TotalItem;
            int? resAll = GetModel(target.list(null))?.PagingInfo.TotalItem;

            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resAll);
        }
    }
}
