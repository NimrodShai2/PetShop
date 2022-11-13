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

        public async Task<IActionResult> Index([FromRoute] int id, [FromQuery] int number)
        {
            ViewBag.Categories = _animalRepo.GetCategories();
           
            if (id == 0)
            { 
                var num = _animalRepo.GetAll().Count() / Constants.Constants.NumberOfElementsInPage;//Determine number of pages needed, passed as a parameter for the view to create
                if (_animalRepo.GetAll().Count() % Constants.Constants.NumberOfElementsInPage == 0)
                {
                    ViewBag.NumberOfPages = num;
                }
                else
                {
                    ViewBag.NumberOfPages = num + 1;
                }

                ViewBag.SelectedCat = 0;
                return View(await _animalRepo.GetNumberFromFullAsync(number));
            }
            else //The same function, but with categorized animals.
            {
                var animals = await _animalRepo.GetAllAsync();
                var chosen = animals.Where(c => c.CategoryId == id);
                var num = chosen.Count() / Constants.Constants.NumberOfElementsInPage;
                if (chosen.Count() % Constants.Constants.NumberOfElementsInPage == 0)
                {
                    ViewBag.NumberOfPages = num;
                }
                else
                {
                    ViewBag.NumberOfPages = num + 1;
                }
                chosen = chosen.Skip(number * Constants.Constants.NumberOfElementsInPage)
                    .Take(Constants.Constants.NumberOfElementsInPage).
                    ToList();
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
        public IActionResult AddComment(int id, string? text)
        {
            if (text == null || text == string.Empty)         //Validation for the comment
            {
                TempData["CommentError"] = "Error";
                return RedirectToAction("ViewDetails");
            }
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
