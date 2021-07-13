using Dapper;
using DatabaseSQL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebMVC.Common;
using WebMVC.Models;

namespace WebMVC.Service
{
    public class ProductService : IProductService
    {
        private readonly IStorageService _storageService;

        public ProductService(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<bool> Create(ProductCreateRequest request)
        {
            if (request.Image == null)
            {
                return false;
            }
            DynamicParameters param = new DynamicParameters();
            param.Add("@Name", request.Name);
            param.Add("@Image", await this.SaveFile(request.Image));
            param.Add("@Price", request.Price);
            DapperORM.ExecuteWithoutReturn<ProductModel>("ProductCreate", param);
            return true;
        }

        public async Task<List<ProductModel>> GetAll()
        {
            return DapperORM.ReturnList<ProductModel>("ProductViewAll", null);
        }

        public async Task<PageResult<ProductModel>> GetAll1(PagingRequest request)
        {
            var data = DapperORM.ReturnList<ProductModel>("ProductViewAll", null);
            int totalRow = data.Count();
            var result = data.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new ProductModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Image = _storageService.GetFileUrl(x.Image)
                }).ToList();
            var pagedResult = new PageResult<ProductModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = result
            };
            return pagedResult;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{originalFileName}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}