using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Models;
using System.Security.Permissions;
using PetShop.Constants;

namespace PetShop.Repositories
{
    public class AnimalRepository : IRepository
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

        /// <summary>
        /// Gets the top two animals with the most comments.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Animal> GetTopTwo()
        {
            return _context.Animals!.Include(a => a.Comments).OrderByDescending(c => c.Comments!.Count).Take(2).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>All of the animals</returns>

        public IEnumerable<Animal> GetAll()
        {
            return _context.Animals!.ToList();
        }

        /// <summary>
        /// Gets the name of the category by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetForeignTitle(int id)
        {
            Category c = _context.Categories!.First(c => c.Id == id);
            return c.Name!;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
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
            return await _context.Animals!.ToListAsync();
        }
        /// <summary>
        /// Gets the top two animal with the most commes, asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Animal>> GetTopTwoAsync()
        {
            return await _context.Animals!.Include(a => a.Comments).OrderByDescending(a => a.Comments!.Count).Take(2).ToListAsync();
        }

        public async Task<Animal?> GetAnimalAsync(int id)
        {
            return await _context.Animals!.Include(a => a.Comments).FirstOrDefaultAsync(a => a.Id == id);
        }

        /// <summary>
        /// Skips a specified number of elements in the animals list, and takes the amount specified in Constants class.
        /// </summary>
        /// <param name="numberToSkip">Number of elements to skip</param>
        /// <returns></returns>
        public async Task<IEnumerable<Animal>> GetNumberFromFullAsync(int numberToSkip)
        {
            var toTake = Constants.Constants.NumberOfElementsInPage;
            return await _context.Animals!.
                Skip(numberToSkip * toTake).
                Take(toTake).
                ToListAsync();
        }
    }
}
