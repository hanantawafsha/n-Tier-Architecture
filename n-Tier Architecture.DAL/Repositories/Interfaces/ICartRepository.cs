using n_Tier_Architecture.DAL.Models;


namespace n_Tier_Architecture.DAL.Repositories.Interfaces
{
    public interface ICartRepository
    {
       Task<int> AddAsync(Cart cart);
        Task<List<Cart>> GetUserCartAsync(string UserId);
        Task<bool> ClearCartAsync(string UserId);
    }
}
