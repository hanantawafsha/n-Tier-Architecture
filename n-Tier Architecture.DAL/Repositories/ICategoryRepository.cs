using n_Tier_Architecture.DAL.Models;

namespace n_Tier_Architecture.DAL.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll(bool withTracking = false);
        Category? GetById(int id);
        int Add(Category category);
        int Update(Category category);
        int Delete(Category category);
       // bool ToggleStatus(int id);


    }
}