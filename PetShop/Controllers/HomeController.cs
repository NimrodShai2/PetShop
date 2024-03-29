﻿using Microsoft.AspNetCore.Mvc;
using PetShop.Models;
using PetShop.Repositories;

namespace PetShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repo;
        public HomeController(IRepository repository)
        {
            _repo = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetTopTwoAsync());
        }
    }
}
