using AGL.DEV.Model;
using AGL.DEV.Repository;
using AGL.DEV.Web.Models;
using AGL.DEV.Web.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AGL.DEV.Web.Test
{
    [TestFixture]
    public class ServiceUnitTest
    {
        private List<Person> _personData;
        private List<string> _maleOwnerPetNames;
        private List<string> _femaleOwnerPetNames;
        private List<string> _maleOwnerPetNamesSorted;
        private List<string> _femaleOwnerPetNamesSorted;
        private List<string> _maleOwnerPetCatNames;
        private List<string> _femaleOwnerPetCatNames;
        private List<string> _maleOwnerPetCatNamesSorted;
        private List<string> _femaleOwnerPetCatNamesSorted;

        [OneTimeSetUp]
        public void Initialize()
        {
            _personData = new List<Person>()
            {
                new Person() { Name = "Alice", Age = 64, Gender = Gender.Female,
                    Pets = new List<Pet>()
                    {
                        new Pet() { Name = "Simba", Type = PetType.Cat },
                        new Pet() { Name = "Nemo", Type = PetType.Fish },
                        new Pet() { Name = "Jim", Type = PetType.Cat }
                    } },
                new Person() { Name = "Bob", Age = 23, Gender = Gender.Male,
                    Pets = new List<Pet>()
                    {
                        new Pet() { Name = "Garfield", Type = PetType.Cat },
                        new Pet() { Name = "Fido", Type = PetType.Dog },
                        new Pet() { Name = "Tom", Type = PetType.Cat },
                    } },
                new Person() { Name = "Steve", Age = 45, Gender = Gender.Male },
            };

            _maleOwnerPetNames = new List<string>() { "Garfield", "Fido", "Tom" };
            _femaleOwnerPetNames = new List<string>() { "Simba", "Nemo", "Jim" };
            _maleOwnerPetNamesSorted = new List<string>() { "Fido", "Garfield", "Tom" };
            _femaleOwnerPetNamesSorted = new List< string > () { "Jim", "Nemo", "Simba"};
            _maleOwnerPetCatNames = new List<string>() { "Garfield", "Tom" };
            _femaleOwnerPetCatNames = new List<string>() { "Simba", "Jim" };
            _maleOwnerPetCatNamesSorted = new List<string>() { "Garfield", "Tom" };
            _femaleOwnerPetCatNamesSorted = new List<string>() { "Jim", "Simba" };
        }

        [Test]
        public async Task Service_Should_Return_ViewModel()
        {
            // Arrange
            Mock<IRepository> mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.GetPeopleData()).Returns(async () => { await Task.Yield(); return _personData; });

            // Act
            IService service = new Service(mockRepository.Object);
            PetNameViewModel viewModel = await service.GetPetNameViewModel(PetType.Cat);

            // Assert
            // using of multiple asserts as they are testing the same thing (If the first assert fails, it means the second would fail too)
            Assert.IsNotNull(viewModel);
            Assert.IsNotNull(viewModel.FemaleOwnerPetNames);
            Assert.IsNotNull(viewModel.MaleOwnerPetNames);
        }

        [Test]
        public async Task Service_Returned_ViewModel_Has_Only_Cat_Names()
        {
            // Arrange
            Mock<IRepository> mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.GetPeopleData()).Returns(async () => { await Task.Yield(); return _personData; });
            PetNameViewModel expectedViewModel = new PetNameViewModel() {
                FemaleOwnerPetNames = _femaleOwnerPetCatNames,
                MaleOwnerPetNames = _maleOwnerPetCatNames
            };
            
            // Act
            IService service = new Service(mockRepository.Object);
            PetNameViewModel actualViewModel = await service.GetPetNameViewModel(PetType.Cat);

            // Assert
            CollectionAssert.AreEquivalent(expectedViewModel.FemaleOwnerPetNames, actualViewModel.FemaleOwnerPetNames);
            CollectionAssert.AreEquivalent(expectedViewModel.MaleOwnerPetNames, actualViewModel.MaleOwnerPetNames);
        }

        [Test]
        public async Task Service_Returned_ViewModel_Has_Only_Sorted_Cat_Names()
        {
            // Arrange
            Mock<IRepository> mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.GetPeopleData()).Returns(async () => { await Task.Yield(); return _personData; });
            PetNameViewModel expectedViewModel = new PetNameViewModel()
            {
                FemaleOwnerPetNames = _femaleOwnerPetCatNamesSorted,
                MaleOwnerPetNames = _maleOwnerPetCatNamesSorted
            };

            // Act
            IService service = new Service(mockRepository.Object);
            PetNameViewModel actualViewModel = await service.GetPetNameViewModel(PetType.Cat);
            
            // Assert
            CollectionAssert.AreEqual(expectedViewModel.FemaleOwnerPetNames, actualViewModel.FemaleOwnerPetNames);
            CollectionAssert.AreEqual(expectedViewModel.MaleOwnerPetNames, actualViewModel.MaleOwnerPetNames);
        }

        [Test]
        public async Task Service_Returned_ViewModel_Not_Only_Has_Cat_Names()
        {
            // Arrange
            Mock<IRepository> mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.GetPeopleData()).Returns(async () => { await Task.Yield(); return _personData; });
            PetNameViewModel expectedViewModel = new PetNameViewModel()
            {
                FemaleOwnerPetNames = _femaleOwnerPetNames,
                MaleOwnerPetNames = _maleOwnerPetNames
            };
            
            // Act
            IService service = new Service(mockRepository.Object);
            PetNameViewModel actualViewModel = await service.GetPetNameViewModel(PetType.Cat);
            
            // Assert
            CollectionAssert.AreNotEquivalent(expectedViewModel.FemaleOwnerPetNames, actualViewModel.FemaleOwnerPetNames);
            CollectionAssert.AreNotEquivalent(expectedViewModel.MaleOwnerPetNames, actualViewModel.MaleOwnerPetNames);
        }
    }
}
