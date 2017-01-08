using System.Collections.Generic;

namespace AGL.DEV.Web.Models
{
    public class PetNameViewModel
    {
        public List<string> MaleOwnerPetNames { get; set; }
        public List<string> FemaleOwnerPetNames { get; set; }

        public PetNameViewModel()
        {
            MaleOwnerPetNames = new List<string>();
            FemaleOwnerPetNames = new List<string>();
        }
    }
}