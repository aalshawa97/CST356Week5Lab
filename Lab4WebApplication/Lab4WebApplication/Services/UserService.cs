using Lab4WebApplication.Data.Entities;
using Lab4WebApplication.Models.View;
using Lab4WebApplication.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab4WebApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IEntity _repository;

        public UserService(IEntity userRepository)
        {
            _repository = userRepository;
        }

        public UserViewModel GetUser(int id)
        {
            var user = _repository.GetUser(id);

            return (MapToUserViewModel(user));
        }

        public IEnumerable<UserViewModel> GetAllUsers()
        {
            var userViewModels = new List<UserViewModel>();

            var users = _repository.GetAllUsers();
            foreach (var user in users)
            {
                userViewModels.Add(MapToUserViewModel(user));
            }

            return userViewModels;
        }

        public void SaveUser(UserViewModel userViewModel)
        {
            _repository.SaveUser(MapToUser(userViewModel));
        }

        public void UpdateUser(UserViewModel userViewModel)
        {
            var user = _repository.GetUser(userViewModel.Id);
            CopyToUser(userViewModel, user);

            _repository.UpdateUser(user);
        }

        public void DeleteUser(int id)
        {
            _repository.DeleteUser(id);
        }

        private User MapToUser(UserViewModel userViewModel)
        {
            return new User
            {
                Id = userViewModel.Id,
                FirstName = userViewModel.FirstName,
                MiddleName = userViewModel.MiddleName,
                LastName = userViewModel.LastName,
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
            user.EmailAddress = userViewModel.EmailAddress;
            user.DateOfBirth = userViewModel.DOB;
            user.YearsInSchool = userViewModel.YearsInSchool;
        }
    }
}