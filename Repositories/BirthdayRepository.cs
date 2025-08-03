using Birthday_tracker.Models;
using Birthday_tracker.Data;
using Microsoft.EntityFrameworkCore;

namespace Birthday_tracker.Repositories
{
    public class BirthdayRepository : IBirthdayRepository
    {
        private readonly ApplicationDbContext _context;

        public BirthdayRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Birthday birthday)
        {
            _context.Birthdays.Add(birthday);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Birthday birthday)
        {
            _context.Birthdays.Remove(birthday);
            await _context.SaveChangesAsync();
        }

        public Task<List<Birthday>> GetAllAsync()
        {
            return _context.Birthdays.ToListAsync();
        }

        public async Task<List<Birthday>> GetFilteredByDateAsync(DateTime date1, DateTime date2)
        {
            var startDayOfYear = date1.DayOfYear;
            var endDayOfYear = date2.DayOfYear;

            if (startDayOfYear > endDayOfYear)
            {   // If year flipping
                return await _context.Birthdays
                    .Where(b => 
                        b.BirthdayDate.DayOfYear >= startDayOfYear || 
                        b.BirthdayDate.DayOfYear <= endDayOfYear)
                    .OrderBy(b => b.BirthdayDate)
                    .ToListAsync();
            }
            else
            {   // If birthdays in same year
                return await _context.Birthdays
                    .Where(b => 
                        b.BirthdayDate.DayOfYear >= startDayOfYear && 
                        b.BirthdayDate.DayOfYear <= endDayOfYear)
                    .OrderBy(b => b.BirthdayDate)
                    .ToListAsync();
            }
        }

        public async Task<List<Birthday>> GetSortedAsync(string field)
        {
            return field switch
            {
                "name" => await _context.Birthdays.OrderBy(b => b.Name).ToListAsync(),
                "id" => await _context.Birthdays.OrderBy(b => b.Id).ToListAsync(),
                _ => await _context.Birthdays.OrderBy(b => b.BirthdayDate).ToListAsync()
            };
        }

        public Task<Birthday?> FindAsync(string name, DateTime date)
        {
            return _context.Birthdays.FirstOrDefaultAsync(p => p.Name == name && p.BirthdayDate == date);
        }
    }
}
