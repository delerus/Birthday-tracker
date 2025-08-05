using Birthday_tracker.Models;

namespace Birthday_tracker.Repositories
{
    public interface IBirthdayRepository
    {
        Task<List<Birthday>> GetAllAsync();
        Task<List<Birthday>> GetSortedAsync(string field);
        Task<List<Birthday>> GetFilteredByDateAsync(DateTime date1, DateTime date2);
        Task AddAsync(Birthday birthday);
        Task DeleteAsync(Birthday birthday);
        Task<Birthday?> FindAsync(string name, DateTime date);
        Task<Birthday?> FindAsync(int id);
    }
}
