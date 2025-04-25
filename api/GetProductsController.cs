using Microsoft.AspNetCore.Mvc;
using minimalistiskdotcom.api.Models;
using minimalistiskdotcom.api.Service;
using Umbraco.Cms.Web.Common.Controllers;

namespace YourProject.Controllers
{
    //Med Javascript
    //[Route("api/[controller]")]
    //public class ProductApiController : UmbracoApiController
    //{
    //    private readonly MockProductService _productService;

    //    public ProductApiController()
    //    {
    //        _productService = new MockProductService();
    //    }

    //    [HttpGet]
    //    public ActionResult<List<Product>> GetProducts()
    //    {
    //        var products = _productService.GetProducts();
    //        return Ok(products);
    //    }
    //}

    //Til Dependency Injection
    public class ProductController : Controller
    {
        private readonly MockProductService _productService;

        public ProductController(MockProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetProducts();
            return View(products);
        }
    }
}
