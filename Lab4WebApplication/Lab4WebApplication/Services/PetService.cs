using Lab4WebApplication.Data.Entities;
using Lab4WebApplication.Models.View;
using Lab4WebApplication.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab4WebApplication.Services
{
    public class PetService : IPetService
    {
        private readonly IEntity _repository;

        public PetService(IEntity respository)
        {
            _repository = respository;
        }

        public PetViewModel GetPet(int id)
        {
            var pet = _repository.GetPet(id);
            return MapToPetViewModel(pet);
        }

        public IEnumerable<PetViewModel> GetPetsForUser(int userId)
        {
            var petViewModels = new List<PetViewModel>();

            var pets = _repository.GetPetsForUser(userId);

            foreach (var pet in pets)
            {
                petViewModels.Add(MapToPetViewModel(pet));
            }

            return petViewModels;
        }

        public void SavePet(PetViewModel petViewModel)
        {
            //throw new Exception("Test Exception");

            var pet = MapToPet(petViewModel);

            _repository.SavePet(pet);
        }

        public void UpdatePet(PetViewModel petViewModel)
        {
            var pet = _repository.GetPet(petViewModel.Id);

            CopyToPet(petViewModel, pet);

            _repository.UpdatePet(pet);
        }

        public void DeletePet(int id)
        {
            _repository.DeletePet(id);
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
            var petViewModel = new PetViewModel
            {
                Id = pet.Id,
                Name = pet.Name,
                Age = pet.Age,
                NextCheckup = pet.NextCheckup,
                VetName = pet.VetName,
                UserId = pet.UserId
            };

            petViewModel.CheckupAlert = (petViewModel.NextCheckup - DateTime.Now).Days < 14;

            return petViewModel;
        }

        private void CopyToPet(PetViewModel petViewModel, Pet pet)
        {
            pet.Id = petViewModel.Id;
            pet.Name = petViewModel.Name;
            pet.Age = petViewModel.Age;
            pet.NextCheckup = petViewModel.NextCheckup;
            pet.VetName = petViewModel.VetName;
            pet.UserId = petViewModel.UserId;
        }
    }
}