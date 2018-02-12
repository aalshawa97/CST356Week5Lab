using Common.Logging;
using Lab4WebApplication.Data;
using Lab4WebApplication.Data.Entities;
using Lab4WebApplication.Models.View;
using Lab4WebApplication.Repositories;
using Lab4WebApplication.Services;
using SimpleInjector;
using System;
using System.Collections.Generic;
using log4net;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab4WebApplication.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetService petService;
        private readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(PetController));

        public PetController(IPetService petService)
        {
            this.petService = petService;
        }

        //Create a SimpleInjector container
        static Container container = new Container();

        //Configure the container
        EntityRepository entityRepository = (EntityRepository)container.GetInstance(typeof(EntityRepository));

        //static AppDbContext dbContext;
        //EntityRepository entityRepository = new EntityRepository(dbContext);

        public ActionResult List(int userId)
        {
            ViewBag.UserId = userId;

            var pets = GetPetsForUser(userId);

            return View(pets);
        }

        [HttpGet]
        public ActionResult Create(int userId)
        {
            ViewBag.UserId = userId;

            return View();
        }

        [HttpPost]
        public ActionResult Create(PetViewModel petViewModel)
        {
            if (ModelState.IsValid)
            {
                Save(petViewModel);
                return RedirectToAction("List", new { UserId = petViewModel.UserId });
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var pet = GetPet(id);

            return View(pet);
        }

        [HttpPost]
        public ActionResult Edit(PetViewModel petViewModel)
        {
            petViewModel.UserId = GetPet(petViewModel.Id).UserId;
            if (ModelState.IsValid)
            {
                petService.UpdatePet(petViewModel);

                return RedirectToAction("List", new { petViewModel.UserId });
            }

            return View();
        }

        public ActionResult Details(int id)
        {
            var pet = GetPet(id);

            return View(pet);
        }

        private void UpdatePet(PetViewModel petViewModel)
        {

            var pet = entityRepository.GetPet(petViewModel.Id);

            CopyToPet(petViewModel,pet);
        
            entityRepository.UpdatePet(pet);
        }

        private void CopyToPet(PetViewModel petViewModel, Pet pet)
        {
            pet.Name = petViewModel.Name;
            pet.Age = petViewModel.Age;
            pet.NextCheckup = petViewModel.NextCheckup;
            pet.VetName = pet.VetName;
        }

        public ActionResult Delete(int id)
        {
            var pet = GetPet(id);

            DeletePet(id);

            return RedirectToAction("List", new { UserId = pet.UserId });
        }

        private PetViewModel GetPet(int petId)
        {
          var pet = entityRepository.GetPet(petId);
          //var pet = petService.GetPet(petId);

          return MapToPetViewModel(pet);
        }

        private ICollection<PetViewModel> GetPetsForUser(int userId)
        {
            var petViewModels = new List<PetViewModel>();

            var dbContext = new AppDbContext();

            var pets = dbContext.Pets.Where(pet => pet.UserId == userId).ToList();

            foreach (var pet in pets)
            {
                var petViewModel = MapToPetViewModel(pet);
                petViewModels.Add(petViewModel);
            }

            return petViewModels;
        }

        private void Save(PetViewModel petViewModel)
        {
            var pet = MapToPet(petViewModel);

            entityRepository.SavePet(pet);
        }

        private void DeletePet(int id)
        {
            entityRepository.DeletePet(id);
        }

        private Pet MapToPet(PetViewModel petViewModel)
        {
            return new Pet
            {
                Id = petViewModel.Id,
                Name = petViewModel.Name,
                Age = petViewModel.Age,
                NextCheckup = petViewModel.NextCheckup,
                VetName = petViewModel.VetName,
                UserId = petViewModel.UserId
            };
        }

        private PetViewModel MapToPetViewModel(Pet pet)
        {
            return new PetViewModel
            {
                Id = pet.Id,
                Name = pet.Name,
                Age = pet.Age,
                NextCheckup = pet.NextCheckup,
                VetName = pet.VetName,
                UserId = pet.UserId
            };
        }
    }
}
