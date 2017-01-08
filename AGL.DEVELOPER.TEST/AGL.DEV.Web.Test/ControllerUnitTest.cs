using AGL.DEV.Model;
using AGL.DEV.Web.Controllers;
using AGL.DEV.Web.Models;
using AGL.DEV.Web.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AGL.DEV.Web.Test
{
    [TestFixture]
    public class ControllerUnitTest
    {
        private PetNameViewModel _petNameViewModel;
        private PetNameViewModel _petCatNameViewModel;
        private PetNameViewModel _petCatNameSortedViewModel;

        [OneTimeSetUp]
        public void Initialize()
        {
            _petNameViewModel = new PetNameViewModel()
            {
                FemaleOwnerPetNames = new List<string>() { "Simba", "Nemo", "Jim" },
                MaleOwnerPetNames = new List<string>() { "Garfield", "Fido", "Tom" }
            };

            _petCatNameViewModel = new PetNameViewModel()
            {
                FemaleOwnerPetNames = new List<string>() { "Simba", "Jim" },
                MaleOwnerPetNames = new List<string>() { "Garfield", "Tom" }
            };

            _petCatNameSortedViewModel = new PetNameViewModel()
            {
                FemaleOwnerPetNames = new List<string>() { "Jim", "Simba" },
                MaleOwnerPetNames = new List<string>() { "Garfield", "Tom" }
            };
        }

        [Test]
        public async Task Controller_Should_Return_ViewResult_With_Model()
        {
            // Arrange
            Mock<IService> mockService = new Mock<IService>();
            mockService.Setup(x => x.GetPetNameViewModel(PetType.Cat))
                                    .Returns(async () =>
                                    {
                                        await Task.Yield();
                                        return _petCatNameViewModel;
                                    });
            // Act
            HomeController controller = new HomeController(mockService.Object);
            ViewResult result = await controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result.ViewData.Model);
        }

        [Test]
        public async Task Controller_Should_Return_ViewResult_With_Valid_Model()
        {
            // Arrange
            Mock<IService> mockService = new Mock<IService>();
            mockService.Setup(x => x.GetPetNameViewModel(PetType.Cat))
                                    .Returns(async () =>
                                    {
                                        await Task.Yield();
                                        return _petCatNameViewModel;
                                    });
            // Act
            HomeController controller = new HomeController(mockService.Object);
            ViewResult result = await controller.Index() as ViewResult;

            // Assert
            Assert.IsInstanceOf<PetNameViewModel>(result.ViewData.Model);
        }

        [Test]
        public async Task Controller_Returned_ViewResult_With_Model_Having_Only_Cat_Names()
        {
            // Arrange
            Mock<IService> mockService = new Mock<IService>();
            mockService.Setup(x => x.GetPetNameViewModel(PetType.Cat))
                                    .Returns(async () =>
                                    {
                                        await Task.Yield();
                                        return _petCatNameViewModel;
                                    });
            PetNameViewModel expectedViewModel = _petCatNameViewModel;
            
            // Act
            HomeController controller = new HomeController(mockService.Object);
            ViewResult result = await controller.Index() as ViewResult;
            PetNameViewModel actualViewModel = result.ViewData.Model as PetNameViewModel;

            // Assert
            CollectionAssert.AreEquivalent(expectedViewModel.FemaleOwnerPetNames, actualViewModel.FemaleOwnerPetNames);
            CollectionAssert.AreEquivalent(expectedViewModel.MaleOwnerPetNames, actualViewModel.MaleOwnerPetNames);
        }

        [Test]
        public async Task Controller_Returned_ViewResult_With_Model_Having_Only_Sorted_Cat_Names()
        {
            // Arrange
            Mock<IService> mockService = new Mock<IService>();
            mockService.Setup(x => x.GetPetNameViewModel(PetType.Cat))
                                    .Returns(async () =>
                                    {
                                        await Task.Yield();
                                        return _petCatNameViewModel;
                                    });
            PetNameViewModel expectedViewModel = _petCatNameSortedViewModel;
            
            // Act
            HomeController controller = new HomeController(mockService.Object);
            ViewResult result = await controller.Index() as ViewResult;
            PetNameViewModel actualViewModel = result.ViewData.Model as PetNameViewModel;

            // Assert
            CollectionAssert.AreEquivalent(expectedViewModel.FemaleOwnerPetNames, actualViewModel.FemaleOwnerPetNames);
            CollectionAssert.AreEquivalent(expectedViewModel.MaleOwnerPetNames, actualViewModel.MaleOwnerPetNames);
        }

        [Test]
        public async Task Controller_Returned_ViewResult_With_Model_Having_Not_Only_Cat_Names()
        {
            // Arrange
            Mock<IService> mockService = new Mock<IService>();
            mockService.Setup(x => x.GetPetNameViewModel(PetType.Cat))
                                    .Returns(async () =>
                                    {
                                        await Task.Yield();
                                        return _petCatNameViewModel;
                                    });
            PetNameViewModel expectedViewModel = _petNameViewModel;
            
            // Act
            HomeController controller = new HomeController(mockService.Object);
            ViewResult result = await controller.Index() as ViewResult;
            PetNameViewModel actualViewModel = result.ViewData.Model as PetNameViewModel;
            
            // Assert
            CollectionAssert.AreNotEquivalent(expectedViewModel.FemaleOwnerPetNames, actualViewModel.FemaleOwnerPetNames);
            CollectionAssert.AreNotEquivalent(expectedViewModel.MaleOwnerPetNames, actualViewModel.MaleOwnerPetNames);
        }

        [Test]
        public async Task Controller_Action_Should_Render_Correct_View()
        {
            // Arrange
            Mock<IService> mockService = new Mock<IService>();
            mockService.Setup(x => x.GetPetNameViewModel(PetType.Cat))
                                    .Returns(async () =>
                                    {
                                        await Task.Yield();
                                        return _petCatNameViewModel;
                                    });
            // Act
            HomeController controller = new HomeController(mockService.Object);
            ViewResult result = await controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
