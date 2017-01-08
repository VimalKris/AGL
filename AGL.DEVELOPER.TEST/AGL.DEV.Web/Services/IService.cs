using AGL.DEV.Model;
using AGL.DEV.Web.Models;
using System.Threading.Tasks;

namespace AGL.DEV.Web.Services
{
    public interface IService
    {
        Task<PetNameViewModel> GetPetNameViewModel(PetType petType);
    }
}
