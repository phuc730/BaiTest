using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Common;
using WebMVC.Models;

namespace WebMVC.Service
{
    public interface IProductService
    {
        public Task<bool> Create(ProductCreateRequest request);

        public Task<List<ProductModel>> GetAll();

        public Task<PageResult<ProductModel>> GetAll1(PagingRequest request);
    }
}