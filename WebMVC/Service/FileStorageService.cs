using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Service
{
    public class FileStorageService : IStorageService
    {
        private readonly string _contentFolder;
        private const string FOLDER_NAME = "product-image";
        private const string LOCALHOST = "https://localhost:5001/";

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _contentFolder = Path.Combine(webHostEnvironment.WebRootPath, FOLDER_NAME);
        }

        public string GetFileUrl(string fileName)
        {
            return $"{LOCALHOST}/{FOLDER_NAME}/{fileName}";
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_contentFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }
    }
}