using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface ICategoryRepository
    {
        //GetAllCategory
        IEnumerable<Category> GetAllCategory();

        //GetById
        Category? GetById(int id);

        //Add
        int Add(Category category);

        //Update
        int Update(Category category);

        //Delete
        int Remove(int id);
    }
}
