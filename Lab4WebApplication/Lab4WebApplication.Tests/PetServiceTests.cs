using System;
using NUnit.Framework;
using Lab4WebApplication.Services;
using Lab4WebApplication.Data.Entities;
using FakeItEasy;
using Lab4WebApplication.Repositories;

namespace Lab4WebApplication.Tests
{
    [TestFixture]
    public class PetServiceTests
    {
        private IEntity _petRespository;

        [SetUp]
        public void SetUp()
        {
            _petRespository = A.Fake<IEntity>();
        }

        [Test]
        public void ShouldNotIndicateCheckupAlert()
        {
            // Arrange
            A.CallTo(() => _petRespository.GetPet(A<int>.Ignored)).Returns(new Pet
            {
                NextCheckup = DateTime.Now.AddDays(29)
            });

            // Act (SUT)
            var petService = new PetService(_petRespository);
            var petViewModel = petService.GetPet(1);

            // Assert
            Assert.IsFalse(petViewModel.CheckupAlert);
        }

        [Test]
        public void ShouldIndicateCheckupAlert()
        {
            // Arrange
            A.CallTo(() => _petRespository.GetPet(A<int>.Ignored)).Returns(new Pet
            {
                NextCheckup = DateTime.Now.AddDays(13)
            });

            // Act (SUT)
            var petService = new PetService(_petRespository);
            var petViewModel = petService.GetPet(1);

            // Assert
            Assert.IsTrue(petViewModel.CheckupAlert);
        }
    }
}
