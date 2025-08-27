using Mapster;
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
    
    }
}
