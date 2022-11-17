using Microsoft.EntityFrameworkCore;
using PetShop.Models;

namespace PetShop.Data
{
    public class PetContext : DbContext
    {
        public PetContext(DbContextOptions<PetContext> options) : base(options)
        {

        }

        public DbSet<Animal>? Animals { get; set; }

        public DbSet<Category>? Categories { get; set; }

        public DbSet<Comment>? Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            Comment c1 = new Comment() { Id = 1, AnimalId = 1, Content = "So Cute!" };
            Comment c2 = new() { Id = 2, AnimalId = 1, Content = "My Baby!" };
            Comment c3 = new() { Id = 3, AnimalId = 2, Content = "Awwww!" };
            Comment c4 = new() { Id = 4, AnimalId = 3, Content = "Is he poisonous?" };
            Comment c5 = new() { Id = 5, AnimalId = 3, Content = "Kermit! Hahaha!" };
            Comment c6 = new() { Id = 6, AnimalId = 3, Content = "Jumpy boi" };
            Comment c7 = new() { Id = 7, AnimalId = 4, Content = "Ewwwww he's digusting!" };
            Comment c8 = new() { Id = 8, AnimalId = 5, Content = "What does he eat?" };
            Comment c9 = new() { Id = 9, AnimalId = 5, Content = "Is that Sid from Ice Age??" };
            Comment c10 = new() { Id = 10, AnimalId = 6, Content = "Look at this little guy!" };
            Comment c11 = new() { Id = 11, AnimalId = 6, Content = "I would eat him now!!!!" };
            Comment c12 = new() { Id = 12, AnimalId = 7, Content = "They are huge!" };
            Comment c13 = new() { Id = 13, AnimalId = 7, Content = "He looks like a dragon" };
            Comment c14 = new() { Id = 14, AnimalId = 7, Content = "What does he eat?" };
            Comment c15 = new() { Id = 15, AnimalId = 7, Content = "Is it even legal to own one of these?" };
            Comment[] c = new Comment[] { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15 };

            modelBuilder.Entity<Category>().HasData(new Category() { Id = 1, Name = "Mammal" },
                new Category() { Id = 2, Name = "Avian" }, new() { Id = 3, Name = "Reptile" },
                new Category()
                {
                    Id = 4,
                    Name = "Amphbian"
                }, new() { Id = 5, Name = "Insect" });



            modelBuilder.Entity<Animal>().HasData(
                new Animal() { Id = 1, Name = "Moshe", Age = 12, CategoryId = 1, Description = "Does he even need a description?", Image = @"\Images\dog.jpeg" },
                new() { Id = 2, Age = 3, CategoryId = 2, Name = "Yonny", Description = "A small cute bird, loves to play!", Image = @"\Images\bird.jpeg" },
                new() { Id = 3, Age = 1, CategoryId = 4, Description = "A young, cute, and energetic little frog!", Name = "Kermit", Image = @"\Images\frog.jpeg" },
                new() { Id = 4, Age = 2, CategoryId = 5, Description = "An evil killer, but look at those eyes!", Image = @"\Images\spider.jpeg", Name = "Peter" },
                new() { Id = 5, Age = 13, Name = "Daniel", CategoryId = 1, Description = "Slowest animal on earth, and the perfect pet for lazy people.", Image = @"\Images\sloth.jpg" },
                new() { Id = 6, Age = 5, CategoryId = 1, Name = "Tom", Description = "He will never listen to you, destroy your property, and is probably evil. A perfect cat for cat lovers!", Image = @"\Images\cat.jpeg" },
                new() { Id = 7, Age = 7, Name = "Ronny", CategoryId = 3, Description = "Godzilla's favorite cousin.", Image = @"\Images\comodo.jpg" },
                new() { Id = 8, Age = 1, Image = @"\Images\eagle.jpg", CategoryId = 2, Name = "Sam", Description = "The American symbol, and quite an impressive pet." },
                new() { Id = 9, Age = 6, Image = @"\Images\capybara.jpg", CategoryId = 1, Name = "Capy", Description = "A big hamster? A fluffy lazy dog? Why not both?!" },
                new() { Id = 10, Age = 13, Image = @"\Images\mantis.jpg", CategoryId = 5, Name = "Shay", Description = "NopeNopeNopeNopeNopeNope" },
                new() { Id = 11, Age = 14, Image = @"\Images\cobra.jpg", CategoryId = 3, Name = "Pharoh", Description = "He is mean an lethal, but also extremley cool." },
                new() { Id = 12, Age = 22, Image = @"\Images\wolf.jpg", CategoryId = 1, Name = "Lupin", Description = "Don't bother him, he's kind of a loner." },
                new() { Id = 13, Age = 132, Image = @"\Images\turtle.jpg", CategoryId = 3, Name = "Donatello", Description = "The largest turtle, and the nicest one." },
                new() { Id = 14, Age = 3, Image = @"\Images\salam.jpg", CategoryId = 4, Name = "Mandy", Description = "Bright, young and beatiful. Also a good friend!" });

            modelBuilder.Entity<Comment>().HasData(c);
        }
    }
}