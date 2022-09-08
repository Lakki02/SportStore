using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using SportSrore.Infrastructure;
using SportSrore.Models.ViewModels;
using Xunit;

namespace SportSore.Test
{
    public class PageLinkTagHelperTest
    {
        [Fact]
        public void Can_Generate_Page_Link()
        {
            //Организация
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("Test/Page1")
                .Returns("Test/Page2")
                .Returns("Test/Page3");

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(f =>
               f.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(urlHelper.Object);

            PageLinkTagHalper helper = 
                new PageLinkTagHalper(urlHelperFactory.Object)
                {
                    PageModel = new PagingInfo
                    {
                        CurrentPage = 2,
                        TotalItem = 28,
                        ItemsPerPage = 10
                    },
                    PageAction = "Test"
                };

            TagHelperContext ctx = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(), "");

            var content = new Mock<TagHelperContent>();
            TagHelperOutput output = new TagHelperOutput("div",
                new TagHelperAttributeList(), (cashe, encoder) => Task.FromResult(content.Object));

            //дейсвтие 
            helper.Process(ctx, output);

            //Утверждение 
            Assert.Equal(@"<a href=""Test/Page1"">1</a>" +
                 @"<a href=""Test/Page2"">2</a>"
               + @"<a href=""Test/Page3"">3</a>",
                        output.Content.GetContent());

        }


    }
}



