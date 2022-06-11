using System.Collections.Generic;
using System.Threading.Tasks;
using cw5.Models;
using cw5.Models.DTO;

namespace cw5.Services
{
    public interface IDbService
    {
        Task<IEnumerable<SomeSortOfTrip>> GetTrips();
        bool DeleteClient(int idClient);
        int AddClient(AdderClient adderClient, int idTrip);
    }
}
