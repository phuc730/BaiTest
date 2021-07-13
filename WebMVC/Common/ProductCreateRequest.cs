using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebMVC.Common
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public IFormFile Image { get; set; }
    }
}