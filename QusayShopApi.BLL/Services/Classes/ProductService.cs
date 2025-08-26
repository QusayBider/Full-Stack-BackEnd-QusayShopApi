using Mapster;
using QusayShopApi.BLL.Services.Interfaces;
using QusayShopApi.DAL.DTO.Requests;
using QusayShopApi.DAL.DTO.Responses;
using QusayShopApi.DAL.Models.Product;
using QusayShopApi.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.BLL.Services.Classes
{
    public class ProductService :GenericService<ProductDTORequest,ProductDTOResponse,Product>, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository Repository , IFileService fileService) : base(Repository)
        {
            _repository = Repository;
            _fileService = fileService;
        }

        public async Task<int> CreateFile(ProductDTORequest request)
        {
            var entity = request.Adapt<Product>();
            entity.Create_at = DateTime.UtcNow;
            if (request.MainImage != null) { 
                
                var imagePath = await _fileService.UploadFileAsync(request.MainImage);
                entity.MainImage = imagePath;   
            }
            return _repository.Update(entity);

        }
    }
}
