using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Implementation
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly GymDbContext _DbContext = new GymDbContext();

        public int Add(Category category)
        {
            _DbContext.Categories.Add(category);
            return _DbContext.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategory() => _DbContext.Categories.ToList();

        public Category? GetById(int id) => _DbContext.Categories.Find(id);

        public int Remove(int id)
        {
            var category = GetById(id);
            if (category is not null)
            {
                _DbContext.Categories.Remove(category);
                return _DbContext.SaveChanges();
            }
            return 0;
        }

        public int Update(Category category)
        {
            var existingCategory = GetById(category.Id);
            if (existingCategory is not null)
            {
                _DbContext.Categories.Update(category);
                return _DbContext.SaveChanges();
            }
            return 0;
        }
    }
}
