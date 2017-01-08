﻿using AGL.DEV.Model;
using AGL.DEV.Web.Models;
using AGL.DEV.Web.Services;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AGL.DEV.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService _service;

        public HomeController(IService service)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            PetNameViewModel viewModel = await _service.GetPetNameViewModel(PetType.Cat);
            return View(viewModel);
        }
    }
}