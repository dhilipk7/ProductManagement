using AutoMapper;
using ProductManagement.Api.Models;
using ProductManagement.Data.Entities;
using ProductManagement.Data.Interfaces;
using ProductManagement.Domain.Interfaces;

namespace ProductManagement.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAllProducts(decimal price)
        {
            List<Product> products=new List<Product>();
            if (price > 0)
            {
                 products = await _productRepository.GetAllByCondition(price);

            }
            else
            {
                 products = await _productRepository.GetAll();
            }
            List<ProductDto> productDtos = _mapper.Map<List<ProductDto>>(products);

            return productDtos;
        }
        public async Task<List<ProductDto>> GetAllProductsByPrice(int price)
        {
            List<Product> products = await _productRepository.GetAllByCondition(price);

            List<ProductDto> productDtos = _mapper.Map<List<ProductDto>>(products);

            return productDtos;
        }
        public async Task<ProductDto> GetProductById(int productId)
        {
            var product = await _productRepository.GetById(productId);

            ProductDto productDto = _mapper.Map<ProductDto>(product);

            return productDto;

        }

        public async Task<ProductDto> AddProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<Product>(productDto);

            var createdProduct = await _productRepository.Add(product);

            ProductDto createdProductDto = _mapper.Map<ProductDto>(createdProduct);
            return createdProductDto;
        }
        public async Task<ProductDto> UpdateProduct(int productId, ProductDto productDto)
        {
            Product product = _mapper.Map<Product>(productDto);

            var updatedProduct = await _productRepository.UpdateById(productId, product);
            if (updatedProduct == null) return null;
            ProductDto updatedProductDto = _mapper.Map<ProductDto>(updatedProduct);

            return updatedProductDto;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            return await _productRepository.DeleteById(id);
        }
    }
}
