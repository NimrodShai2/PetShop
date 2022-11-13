﻿using Microsoft.AspNetCore.Mvc;
using PetShop.Models;
using PetShop.Repositories;
using PetShop.ViewModels;
using System.IO;

namespace PetShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment env;
        private readonly IRepository<Animal> _repo;
        public AdminController(IRepository<Animal> repo, IWebHostEnvironment environment)
        {
            _repo = repo;
            env = environment;
        }
        public async Task<IActionResult> Index()
        {
            var animals = await _repo.GetAllAsync();
            return View(animals);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAnimal(AnimalViewModel model, IFormFile image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (image == null || image.Length == 0)
                    {
                        TempData["Action"] = "Error";
                        return RedirectToAction("Index");
                    }
                    var path = Path.Combine(env.WebRootPath, @"Images\", image.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                        stream.Close();
                    }
                    model.Animal!.Image = @"\Images\" + image.FileName;

                    Animal a = new()
                    {
                        Name = model.Animal!.Name,
                        Age = model.Animal!.Age,
                        Description = model.Animal!.Description,
                        CategoryId = model.Animal!.CategoryId,
                        Image = @"\Images\" + image.FileName
                    };
                    _repo.Add(a);
                    TempData["Action"] = "Added";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Action"] = "Error";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                TempData["Action"] = "Error";
                return RedirectToAction("Index");
            }
        }

        public IActionResult AddAnimal()
        {
            ViewBag.Categories = _repo.GetCategories();
            return View();
        }

        public IActionResult DeleteAnimal(int id)
        {
            try
            {
                _repo.DeleteById(id);
                TempData["Action"] = "Deleted";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Action"] = "Error";
                return RedirectToAction("Index");
            }
        }


        public IActionResult UpdateAnimal(int id)
        {
            ViewBag.Categories = _repo.GetCategories();
            var a = _repo.GetById(id);
            var path = env.WebRootPath + a!.Image!;
            using (var stream = System.IO.File.OpenRead(path!))
            {
                AnimalViewModel model = new()
                {
                    Animal = a,

                    Image = new FormFile(stream, 0, stream.Length, "", Path.GetFileName(stream.Name))
                };
                return View(model);
            }
        }

        public async Task<IActionResult> UpdateAnimalAction(AnimalViewModel model, IFormFile image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (image == null || image.Length == 0)
                    {
                        TempData["Action"] = "Error";
                        return RedirectToAction("Index");
                    }
                    var path = Path.Combine(env.WebRootPath, @"Images\", image.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                        stream.Close();
                    }
                    model.Animal!.Image = @"\Images\" + image.FileName;

                    Animal a = new()
                    {
                        Id = model.Animal.Id,
                        Name = model.Animal!.Name,
                        Age = model.Animal!.Age,
                        Description = model.Animal!.Description,
                        CategoryId = model.Animal!.CategoryId,
                        Image = @"\Images\" + image.FileName
                    };
                    _repo.Update(a);
                    TempData["Action"] = "Saved";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Action"] = "Error";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                TempData["Action"] = "Error";
                return RedirectToAction("Index");
            }
        }

    }
}
