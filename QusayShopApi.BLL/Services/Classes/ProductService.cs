using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
    public class ProductService : GenericService<ProductDTORequest, ProductDTOResponse, Product>, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository Repository, IFileService fileService) : base(Repository)
        {
            _repository = Repository;
            _fileService = fileService;
        }

        public async Task<string> AddProduct(ProductDTORequest request)
        {
            var entity = request.Adapt<Product>();
            entity.Create_at = DateTime.UtcNow;
            if (request.MainImage != null)
            {

                var imagePath = await _fileService.UploadFileAsync(request.MainImage);
                entity.MainImage = imagePath;
            }
            if (request.SubImages != null)
            {

                var subImagePaths = await _fileService.UploadManyFileAsync(request.SubImages);

                entity.SubImages = subImagePaths.Select(img => new ProductImage { ImageName = img }).ToList();
            }
            var ProductId = _repository.Add(entity);
            if (ProductId > 0)
            {
                return $"Product Added Successfully";
            }
            else
            {
                return "Failed to add Product";
            }
        }
        public async Task<List<ProductDTOResponse>> GetAllProductsWithImages(HttpRequest request, int NumberOfPage = 0, int PageSize = 0, bool onlyActive = false)
        {

            var Products = _repository.GetAllProductsWithImages();
            if (onlyActive)
            {
                Products = Products.Where(p => p.Status == DAL.Models.Status.Active).ToList();
            }
            var PagedProducts = Products.Skip((NumberOfPage - 1) * PageSize).Take(PageSize).ToList();

            if (NumberOfPage == 0 && PageSize == 0)
            {

                PagedProducts = Products.ToList();
            }

            return PagedProducts.Select(p => new ProductDTOResponse
            {

                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Rating = p.Rating,
                Quantity = p.Quantity,
                Discount = (decimal)p.Discount,
                Create_at = p.Create_at,
                Update_at = p.Update_at,
                Status = p.Status.ToString(),
                CategoryId = (int)p.CategoryId,
                MainImageUrl = $"{request.Scheme}://{request.Host}/Images/{p.MainImage}",
                SubImagesUrl = p.SubImages.Select(img => $"{request.Scheme}://{request.Host}/Images/{img.ImageName}").ToList(),
                Reviews = p.Reviews.Select(r => new ReviewDTOResponse
                {
                    Id = r.Id,
                    Rating = r.Rate,
                    Comment = r.Comments,
                    ReviewDate = r.ReviewDate,
                    User = r.User.FullName
                }).ToList()
            }).ToList();
        }

        public async Task<ProductDTOResponse> GetProductById(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
            {
                return null;
            }
            return product.Adapt<ProductDTOResponse>();
        }

        public async Task<string> DeleteProduct(int id) { 
         
            var product = await _repository.GetProductById(id);
            if (product == null)
            {
                 return ("Product not found");
            }
            else {
                _fileService.DeleteFileAsync(product.MainImage);
                foreach (var subimage in product.SubImages) {
                    _fileService.DeleteFileAsync(subimage.ImageName);
                }

                var result = _repository.Remove(product);
                return ("Product deleted");
            }
            
        
        }

        public async Task<ProductDTOResponse> GetProductbyIdWithImages(HttpRequest request,int Product_id, bool onlyActive = false)
        {

            var Products = _repository.GetAllProductsWithImages();
            if (onlyActive)
            {
                Products = Products.Where(p => p.Status == DAL.Models.Status.Active).ToList();
            }
            var product = Products.FirstOrDefault(p=>p.Id == Product_id);

            return  new ProductDTOResponse
            {

                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Rating = product.Rating,
                Quantity = product.Quantity,
                Discount = (decimal)product.Discount,
                Create_at = product.Create_at,
                Update_at = product.Update_at,
                Status = product.Status.ToString(),
                CategoryId = (int)product.CategoryId,
                MainImageUrl = $"{request.Scheme}://{request.Host}/Images/{product.MainImage}",
                SubImagesUrl = product.SubImages.Select(img => $"{request.Scheme}://{request.Host}/Images/{img.ImageName}").ToList(),
                Reviews = product.Reviews.Select(r => new ReviewDTOResponse
                {
                    Id = r.Id,
                    Rating = r.Rate,
                    Comment = r.Comments,
                    ReviewDate = r.ReviewDate,
                    User = r.User.FullName
                }).ToList()
            };
        }

    }
}
