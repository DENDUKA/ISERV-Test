using ISERV.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ISERV.Persistence.EF.Services
{
    public class UniversitiesRepository
    {
        private ApplicationContext _context;

        public UniversitiesRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<University>> Find(string country, string universityName)
        {
            var result = await _context.Universities.Where(u => u.Country == country && u.Name == universityName).ToArrayAsync();

            return result.Select(u => new University() 
            { 
                Country = u.Country, 
                Name = u.Name, 
                Sites = u.Sites 
            }).ToList(); //TODO automapper
        }

        public async Task SaveAll(IList<University> universities)
        {
            _context.Universities.AddRange(universities.Select(u => new Entities.University()
            {
                Country = u.Country,
                Name = u.Name,
                Sites = u.Sites
            }
            ).ToList());

            var res = await _context.SaveChangesAsync();
        }
    }
}