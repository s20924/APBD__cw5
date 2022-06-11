using cw5.Models;
using cw5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cw5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public TripsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _dbService.GetTrips();
            return Ok(trips);
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> AddClient(AdderClient adderClient, int idTrip)
        {
            var newClient = _dbService.AddClient(adderClient, idTrip);
            if (newClient == 1)
            {
                return BadRequest("Taki klient juz istnieje");
            }
            else if (newClient == 3)
            {
                return Ok("Poprawnie dodano klienta i dodano go do wycieczki");
            }
            else if (newClient == 2)
            {
                return BadRequest("Taka wycieczka nie istnieje");
            }
            else
            {
                return BadRequest("Problem");
            }
        }

    }
}
