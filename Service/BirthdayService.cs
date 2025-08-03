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
                _repository.DeleteAsync(b);
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
            var allowedFields = new[] { "Name", "BirthdayDate" };
            if (!allowedFields.Contains(field))
                throw new ArgumentException("Invalid sorting field.");

            return _repository.GetSortedAsync(field);
        }
    }
}
