using Birthday_tracker.Models;

namespace Birthday_tracker.Service
{
    public interface IBirthdayService
    {
        Task<List<Birthday>> GetFilteredAsync(DateTime date1, DateTime date2);
        Task<List<Birthday>> GetSortedAsync(string field);
        Task AddBirthdayAsync(Birthday b);
        Task DeleteBirthdayAsync(Birthday b);
    }
}
