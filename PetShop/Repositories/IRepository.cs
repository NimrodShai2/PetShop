using PetShop.Models;

namespace PetShop.Repositories
{
    public interface IRepository
    {
        public Animal? GetById(int id);
        public Animal Add(Animal item);
        public Animal Update(Animal item);
        public Animal? DeleteById(int id);
        public IEnumerable<Animal> GetAll();
        public IEnumerable<Animal> GetTopTwo();
        public string GetForeignTitle(int id);
        public IEnumerable<Comment> AddComment(Comment c);
        public IEnumerable<Category> GetCategories();
        public Task<IEnumerable<Animal>> GetAllAsync();
        public  Task<IEnumerable<Animal>> GetTopTwoAsync();
        public Task<Animal?> GetAnimalAsync(int id);
        public Task<IEnumerable<Animal>> GetNumberFromFullAsync(int numberToSkip);
    }
}
