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
            return await _context.Birthdays.Where(b => b.BirthdayDate >= date1 && b.BirthdayDate <= date2).ToListAsync();
        }

        public async Task<List<Birthday>> GetSortedAsync(string field)
        {
            switch (field)
            {
                case "name":
                    await _context.Birthdays.OrderBy(b => b.Name).ToListAsync(); break;
                case "id":
                    await _context.Birthdays.OrderBy(b => b.Id).ToListAsync(); break;
                case "date":
                default:
                    await _context.Birthdays.OrderBy(b => b.BirthdayDate).ToListAsync(); break;
            }

            return await GetAllAsync();
        }

        Task<Birthday?> FindAsync(string name, DateTime date)
        {
            return _context.Birthdays.FirstOrDefaultAsync(p => p.Name == name && p.BirthdayDate == date);
        }
    }
}
