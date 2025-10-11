using Microsoft.AspNetCore.Http;
using QusayShopApi.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Classes
{
    public class FileService : IFileService
    {
        private string _ImagePath;
        public FileService() {
            _ImagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images");
        }
        public void DeleteFileAsync(string fileName)
        {
            var filepath = Path.Combine(_ImagePath, fileName);
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is null or empty", nameof(file));
            }
            else {
                var FileName = Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "images", FileName);
                using (var stream = File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
                return FileName;
            }


        }
        public async Task<List<string>> UploadManyFileAsync(List<IFormFile> files) {

            var FileNames = new List<string>();

            foreach (var file in files)
            {
                if (file == null || file.Length == 0)
                {
                    throw new ArgumentException("File is null or empty", nameof(file));
                }
                else
                {
                    var FileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", FileName);
                    using (var stream = File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                    FileNames.Add(FileName);
                }
            }

            return FileNames;
        }
    }
}
