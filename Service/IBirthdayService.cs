using Birthday_tracker.Models;

namespace Birthday_tracker.Service
{
    public interface IBirthdayService
    {
        Task<List<Birthday>> GetFilteredAsync(DateTime date1, DateTime date2);
        Task<List<Birthday>> GetSortedAsync(string field);
        Task<List<Birthday>> GetUpcomingBirthdaysAsync(int daysAhead = 30);
        Task<List<Birthday>> GetBirthdaysInMonthAsync(int month);
        Task AddBirthdayAsync(Birthday b);
        Task DeleteBirthdayAsync(int id);
        Task UpdateBirthdayAsync(int id);
        Task<List<Birthday>> GetAllAsync();
    }
}
