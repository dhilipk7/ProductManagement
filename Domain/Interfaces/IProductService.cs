using ProductManagement.Api.Models;
using ProductManagement.Data.Entities;

namespace ProductManagement.Domain.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProducts(decimal price);
        Task<ProductDto> GetProductById(int id);
        Task<ProductDto> AddProduct(ProductDto product);
        Task<ProductDto> UpdateProduct(int productId,ProductDto product);
        Task<bool> DeleteProduct(int productId);
    }
}
