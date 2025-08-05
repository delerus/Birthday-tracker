using Birthday_tracker.Data;
using Birthday_tracker.Models;
using Birthday_tracker.Repositories;

using Microsoft.EntityFrameworkCore;


namespace Birthday_tracker.Service
{
    public class BirthdayService : IBirthdayService
    {
        private readonly IBirthdayRepository _repository;

        public BirthdayService(IBirthdayRepository repository)
        {
            _repository = repository;
        }

        public Task AddBirthdayAsync(Birthday b)
        {
            return _repository.AddAsync(b);
        }

        public async Task DeleteBirthdayAsync(Birthday b)
        {
            Birthday birthday = await _repository.FindAsync(b.Name, b.BirthdayDate);

            if (birthday != null)
            {
                await _repository.DeleteAsync(b);
            }
            else throw new Exception("Data couldn't be found");

        }

        public Task<List<Birthday>> GetFilteredAsync(DateTime date1, DateTime date2)
        {
            if (date1 > date2)
                (date1, date2) = (date2, date1);

            return _repository.GetFilteredByDateAsync(date1, date2);
        }

        public Task<List<Birthday>> GetSortedAsync(string field)
        {
            var allowedFields = new[] { "name", "id", "date" };
            if (!allowedFields.Contains(field))
                throw new ArgumentException("Invalid sorting field.");

            return _repository.GetSortedAsync(field);
        }

        public Task<List<Birthday>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<List<Birthday>> GetUpcomingBirthdaysAsync(int daysAhead = 7)
        {
            DateTime date1 = DateTime.Now;
            DateTime date2 = date1.AddDays(daysAhead);
            return _repository.GetFilteredByDateAsync(date1, date2);
        }

        public Task<List<Birthday>> GetBirthdaysInMonthAsync(int month)
        {
            if (month < 1 || month > 12)
                throw new ArgumentException("Month must be between 1 and 12.");
            DateTime date1 = new DateTime(DateTime.Now.Year, month, 1);
            DateTime date2 = new DateTime(DateTime.Now.Year, month, DateTime.DaysInMonth(DateTime.Now.Year, month));

            return _repository.GetFilteredByDateAsync(date1, date2);
        }
    }
}
