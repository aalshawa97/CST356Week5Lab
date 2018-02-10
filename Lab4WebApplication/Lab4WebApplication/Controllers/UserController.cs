using Lab4WebApplication.Data;
using Lab4WebApplication.Data.Entities;
using Lab4WebApplication.Models.View;
using Lab4WebApplication.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab4WebApplication.Controllers
{
    public class UserController : Controller
    {
        static AppDbContext dbContext;
        EntityRepository entityRepository = new EntityRepository(dbContext);

        public ActionResult List()
        {
            var users = GetAllUsers();

            return View(users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = MapToUser(userViewModel);

                SaveUser(user);

                return RedirectToAction("List");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            var user = GetUser(id);

            return View(user);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = GetUser(id);

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                UpdateUser(userViewModel);

                return RedirectToAction("List");
            }

            return View();
        }

        public ActionResult Delete(int id)
        {
            DeleteUser(id);

            return RedirectToAction("List");
        }

        private UserViewModel GetUser(int id)
        {
            var user = entityRepository.GetUser(id);

            return MapToUserViewModel(user);
        }

        private IEnumerable<UserViewModel> GetAllUsers()
        {
            return entityRepository.GetAllUsers() as IEnumerable<UserViewModel>;
        }

        private void SaveUser(User user)
        {
            entityRepository.SaveUser(user);
        }

        private void UpdateUser(UserViewModel userViewModel)
        {
            var user = entityRepository.GetUser(userViewModel.Id);

            entityRepository.UpdateUser(user);
        }

        private void DeleteUser(int id)
        {
            entityRepository.DeleteUser(id);
        }

        private User MapToUser(UserViewModel userViewModel)
        {
            return new User
            {
                Id = userViewModel.Id,
                FirstName = userViewModel.FirstName,
                MiddleName = userViewModel.MiddleName,
                LastName = userViewModel.LastName,
                FavoriteHobby = userViewModel.FavoriteHobby,
                EmailAddress = userViewModel.EmailAddress,
                DateOfBirth = userViewModel.DOB,
                YearsInSchool = userViewModel.YearsInSchool
            };
        }

        private UserViewModel MapToUserViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                FavoriteHobby = user.FavoriteHobby,
                EmailAddress = user.EmailAddress,
                DOB = user.DateOfBirth,
                YearsInSchool = user.YearsInSchool
            };
        }

        private void CopyToUser(UserViewModel userViewModel, User user)
        {
            user.FirstName = userViewModel.FirstName;
            user.MiddleName = userViewModel.MiddleName;
            user.LastName = userViewModel.LastName;
            user.FavoriteHobby = userViewModel.FavoriteHobby;
            user.EmailAddress = userViewModel.EmailAddress;
            user.DateOfBirth = userViewModel.DOB;
            user.YearsInSchool = userViewModel.YearsInSchool;
        }
    }
}
