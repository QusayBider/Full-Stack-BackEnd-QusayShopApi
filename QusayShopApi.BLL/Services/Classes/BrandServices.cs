using Azure.Core;
using Mapster;
using Microsoft.AspNetCore.Http;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.Data;
using QusayShopApi.DAL.DTO.Requests;
using QusayShopApi.DAL.DTO.Responses;
using QusayShopApi.DAL.Models;
using QusayShopApi.DAL.Models.Brand;
using QusayShopApi.DAL.Models.Category;
using QusayShopApi.DAL.Models.Product;
using QusayShopApi.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Classes
{
    public class BrandServices : GenericService<BrandDTORequest, BrandDTOResponses, Brand>, IBrandServices
    {
        private readonly IBrandRepository _repository;
        private readonly IFileService _fileService;

        public BrandServices(IBrandRepository Repository,IFileService fileService) : base(Repository)
        {
            _repository = Repository;
            _fileService = fileService;
        }
        public async Task<int> CreateFile(BrandDTORequest request)
            {
                var entity = request.Adapt<Brand>();
                entity.Create_at = DateTime.UtcNow;
                if (request.MainImage != null)
                {

                    var imagePath = await _fileService.UploadFileAsync(request.MainImage);
                    entity.MainImage = imagePath;
                }
                return _repository.Update(entity);

            }

        public bool checkedIfBrandHasExist(string BrandName) { 
            return _repository.checkedIfBrandHasExist(BrandName);

        }
        public async Task<List<BrandDTOResponses>> GetAllProduct(HttpRequest request, bool Brand_InActive=false) {
            var Brands = await _repository.GetAllBrands();

            if (!Brand_InActive)
            {
                Brands = Brands.Where(b => b.Status == Status.Active).ToList(); 
            }

            return  Brands.Select(b => new BrandDTOResponses
            {
                Id = b.Id,
                Name = b.Name,
                Image = $"{request.Scheme}://{request.Host}/Images/{b.MainImage}",
                Status=b.Status.ToString(),
                Create_at = b.Create_at
            }).ToList();
        }
        public async Task<BrandDTOResponses> GetBrandById(HttpRequest request, int id, bool Brand_InActive = false)
        {
            var brand = await _repository.GetBrandByIdAsync(id);

            if (brand == null) return null; 

            if (!Brand_InActive && brand.Status==Status.In_Active)
                return null; 

            return new BrandDTOResponses
            {
                Id = brand.Id,
                Name = brand.Name,
                Image = $"{request.Scheme}://{request.Host}/Images/{brand.MainImage}",
                Status= brand.Status.ToString(),
            };
        }

    }
}
