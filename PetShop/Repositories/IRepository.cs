using PetShop.Models;

namespace PetShop.Repositories
{
    public interface IRepository<T>
    {
        public T? GetById(int id);
        public T Add(T item);
        public T Update(T item);
        public T? DeleteById(int id);
        public IEnumerable<T> GetAll();
        public IEnumerable<T> GetTopTwo();
        public string GetForeignTitle(int id);
        public IEnumerable<Comment> AddComment( Comment c);
        public IEnumerable<Category> GetCategories();
        public Task<IEnumerable<T>> GetAllAsync();
        public  Task<IEnumerable<T>> GetTopTwoAsync();
        public Task<Animal?> GetAnimalAsync(int id);
    }
}
