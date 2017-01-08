using AGL.DEV.Model;
using AGL.DEV.Repository;
using AGL.DEV.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGL.DEV.Web.Services
{
    public class Service : IService
    {
        private readonly IRepository _repository;

        public Service(IRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository");

            _repository = repository;
        }

        public async Task<PetNameViewModel> GetPetNameViewModel(PetType petType)
        {
            List<Person> personData = await _repository.GetPeopleData();

            PetNameViewModel viewModel = new PetNameViewModel()
            {
                MaleOwnerPetNames = FilterAndSortData(personData, Gender.Male, petType),
                FemaleOwnerPetNames = FilterAndSortData(personData, Gender.Female, petType)
            };

            return viewModel;
        }

        private List<string> FilterAndSortData(List<Person> personData, Gender gender, PetType petType, bool isDescending = false)
        {
            List<string> pets = new List<string>();

            if (!isDescending)
            {
                pets = (from person in personData
                        where person.Gender == gender
                        from pet in person.Pets
                        where pet.Type == petType
                        orderby pet.Name ascending
                        select pet.Name).ToList();
            }
            else
            {
                pets = (from person in personData
                        where person.Gender == gender
                        from pet in person.Pets
                        where pet.Type == petType
                        orderby pet.Name descending
                        select pet.Name).ToList();
            }

            return pets;
        }
    }
}