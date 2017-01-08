using AGL.DEV.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AGL.DEV.Repository
{
    public interface IRepository
    {
        Task<List<Person>> GetPeopleData();
    }
}
