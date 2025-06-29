using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepositories _categoryRepositories;

        public CategoryService()
        {
            _categoryRepositories = new CategoryRepositories();
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepositories.GetAllCategories();
        }

        public void InitializeDataset()
        {
            _categoryRepositories.InitializeDataset();
        }

        public Category? GetCategoryById(int id)
        {
            return _categoryRepositories.GetCategoryById(id);
        }

        public bool SaveCategory(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                return false;
            }

            return _categoryRepositories.SaveCategory(category);
        }

        public bool UpdateCategory(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                return false;
            }

            return _categoryRepositories.UpdateCategory(category);
        }

        public bool DeleteCategory(int id)
        {
            return _categoryRepositories.DeleteCategory(id);
        }

        public List<Category> SearchCategories(string keyword)
        {
            return _categoryRepositories.SearchCategories(keyword);
        }
    }
} 