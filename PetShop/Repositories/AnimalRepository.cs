using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Models;
using System.Security.Permissions;

namespace PetShop.Repositories
{
    public class AnimalRepository : IRepository<Animal>
    {
        private readonly PetContext _context;
        public AnimalRepository(PetContext context)
        {
            _context = context;
        }

        public Animal Add(Animal item)
        {
            _context.Animals!.Add(item);
            _context.SaveChangesAsync();
            return item;
        }

        public Animal? DeleteById(int id)
        {
            Animal? item = GetById(id);
            if (item != null)
            {
                _context.Animals!.Remove(item); 
            }
            _context.SaveChangesAsync();
            return item;
        }

        public Animal? GetById(int id)
        {
            return _context.Animals!.Include(c => c.Comments).FirstOrDefault(x => x.Id == id);
        }

        public Animal Update(Animal item)
        {
            var old = _context.Animals!.FirstOrDefault(i => i.Id == item.Id);
            if (old != null)
            {
                _context.Animals!.Remove(old);
                _context.Animals!.Add(item);
            }
            _context.SaveChangesAsync();
            return item;
        }

        public IEnumerable<Animal> GetTopTwo()
        {
            return _context.Animals!.Include(c => c.Comments).OrderByDescending(c => c.Comments!.Count).Take(2).ToList();
        }

        public IEnumerable<Animal> GetAll()
        {
            return _context.Animals!.Include(c => c.Comments).ToList();
        }

        public string GetForeignTitle(int id)
        {
            Category c = _context.Categories!.First(c => c.Id == id);
            return c.Name!;
        }

        public IEnumerable<Comment> AddComment(Comment c)
        {
            _context.Comments!.Add(c);
            _context.SaveChanges();
            return _context.Comments!;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories!.ToList();
        }

        public async Task<IEnumerable<Animal>> GetAllAsync()
        {
            return await _context.Animals!.Include(a => a.Comments).ToListAsync();
        }
        public async Task<IEnumerable<Animal>> GetTopTwoAsync()
        {
            return await _context.Animals!.Include(c => c.Comments).OrderByDescending(c => c.Comments!.Count).Take(2).ToListAsync();
        }

        public async Task<Animal?> GetAnimalAsync(int id)
        {
            return await _context.Animals!.Include(c => c.Comments).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
