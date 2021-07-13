using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Common;
using WebMVC.Models;
using WebMVC.Service;

namespace WebMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 5)
        {
            var request = new PagingRequest()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var data = await _productService.GetAll1(request);
            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(ProductCreateRequest request)
        {
            var result = await _productService.Create(request);
            if (!result)
            {
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}