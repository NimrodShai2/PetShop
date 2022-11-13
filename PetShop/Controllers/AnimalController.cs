using Microsoft.AspNetCore.Mvc;
using PetShop.Models;
using PetShop.Repositories;

namespace PetShop.Controllers
{
    public class AnimalController : Controller
    {
        readonly IRepository<Animal> _animalRepo;
        public AnimalController(IRepository<Animal> animalRepository)
        {
            _animalRepo = animalRepository;
        }

        public async Task<IActionResult> Index(int id)
        {
            ViewBag.Categories = _animalRepo.GetCategories();
            if (id == 0)
            {
                ViewBag.SelectedCat = 0;
                return View(await _animalRepo.GetAllAsync());
            }
            else
            {
                var animals = await _animalRepo.GetAllAsync();
                var chosen = animals.Where(c => c.CategoryId == id);
                return View(chosen);
            }
        }

        public IActionResult ViewDetails(int id)
        {
            var animal = _animalRepo.GetById(id);
            if (animal == null)
            {
                return Content("Sorry! There is no such animal!");
            }
            ViewBag.Category = _animalRepo.GetForeignTitle(animal.CategoryId);
            return View(animal);
        }

        [HttpPost]
        public IActionResult AddComment(int id, string text)
        {
            if (text == null || text == String.Empty)
                return View("ViewDetails");
            var animal = _animalRepo.GetById(id);
            if (animal == null)
            {
                return Content("Something went wrong!");
            }
            _animalRepo.AddComment(new() { AnimalId = id, Content = text });

            return RedirectToAction("ViewDetails", new { id });
        }
    }
}
