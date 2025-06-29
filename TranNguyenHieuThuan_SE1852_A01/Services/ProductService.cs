using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepositories _productRepositories;

        public ProductService()
        {
            _productRepositories = new ProductRepositories();
        }

        public List<Product> GetAllProducts()
        {
            return _productRepositories.GetAllProducts();
        }

        public void InitializeDataset()
        {
            _productRepositories.InitializeDataset();
        }

        public Product? GetProductById(int id)
        {
            return _productRepositories.GetProductById(id);
        }

        public bool SaveProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.ProductName) || product.UnitPrice <= 0)
            {
                return false;
            }

            return _productRepositories.SaveProduct(product);
        }

        public bool UpdateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.ProductName) || product.UnitPrice <= 0)
            {
                return false;
            }

            return _productRepositories.UpdateProduct(product);
        }

        public bool DeleteProduct(int id)
        {
            return _productRepositories.DeleteProduct(id);
        }

        public List<Product> SearchProducts(string keyword)
        {
            return _productRepositories.SearchProducts(keyword);
        }
    }
} 