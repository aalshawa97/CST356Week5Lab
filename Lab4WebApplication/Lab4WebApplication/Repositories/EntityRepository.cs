using Lab4WebApplication.Data;
using Lab4WebApplication.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab4WebApplication.Repositories
{
    public interface IEntity
    {
        Pet GetPet(int id);

        IEnumerable<Pet> GetPetsForUser(int userId);

        void SavePet(Pet pet);

        void UpdatePet(Pet user);

        void DeletePet(int id);

        User GetUser(int id);

        IEnumerable<User> GetAllUsers();

        void SaveUser(User user);

        void UpdateUser(User user);

        void DeleteUser(int id);
    }

    public class EntityRepository:IEntity
    {
        public EntityRepository(AppDbContext appDbContext)
        {
            this.dbContext = appDbContext = new AppDbContext();
        }

        AppDbContext dbContext;

        public Pet GetPet(int id)
        {
            return dbContext.Pets.Find(id);
        }

        public IEnumerable<Pet> GetPetsForUser(int userId)
        {
            return dbContext.Pets.Where(pet => pet.UserId == userId).ToList();
        }

        public void SavePet(Pet pet)
        {
            dbContext.Pets.Add(pet);

            dbContext.SaveChanges();
        }

        public void UpdatePet(Pet pet)
        {
            dbContext.SaveChanges();
        }

        public void DeletePet(int id)
        {
            var pet = dbContext.Pets.Find(id);

            if (pet == null) return;

            dbContext.Pets.Remove(pet);
            dbContext.SaveChanges();
        }
        public User GetUser(int id)
        {
            return dbContext.Users.Find(id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return dbContext.Users;
        }

        public void SaveUser(User user)
        {
            dbContext.Users.Add(user);

            dbContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            dbContext.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = dbContext.Users.Find(id);

            if (user == null) return;

            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }
    }
}