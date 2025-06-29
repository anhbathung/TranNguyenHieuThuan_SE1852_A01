using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Repositories
{
    public interface ICategoryRepositories
    {
        public List<Category> GetAllCategories();
        public void InitializeDataset();
        public Category? GetCategoryById(int id);
        public bool SaveCategory(Category category);
        public bool UpdateCategory(Category category);
        public bool DeleteCategory(int id);
        public List<Category> SearchCategories(string keyword);
    }
} 