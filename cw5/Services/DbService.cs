using cw5.Models;
using cw5.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw5.Services
{
    public class DbService : IDbService
    {
        private readonly _2019SBDContext _dbContext;
        public DbService(_2019SBDContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int AddClient(AdderClient adderClient, int idTrip)
        {
            if(_dbContext.Clients.Any(e => e.Pesel == adderClient.Pesel))
            {
                return 1;
            }
            else
            {
                _dbContext.Clients.Add(new Client(adderClient.FirstName, adderClient.LastName, adderClient.Email, adderClient.Telephone, adderClient.Pesel));
                _dbContext.SaveChanges();
                if(_dbContext.Trips.Any(e => e.IdTrip == idTrip))
                {
                    Client client = _dbContext.Clients.Single(e => e.Pesel == adderClient.Pesel);
                    _dbContext.ClientTrips.Add(new ClientTrip(client.IdClient, idTrip, adderClient.PaymentDate));
                    _dbContext.SaveChanges();
                    return 3;
                    
                }
                else
                {
                    return 2;
                }
            }
        }

        public bool DeleteClient(int idClient)
        {
            try
            {
                if (_dbContext.ClientTrips.Any(e => e.IdClient == idClient))
                {
                    return false;
                }
                else
                {
                    _dbContext.Remove(_dbContext.Clients.Single(e => e.IdClient == idClient));
                    _dbContext.SaveChanges();
                    return true;
                }
            }catch(InvalidOperationException ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<SomeSortOfTrip>> GetTrips()
        {
            return await _dbContext.Trips
                .Include(e => e.CountryTrips)
                .Include(e => e.ClientTrips)
                .Select(e => new SomeSortOfTrip
                {
                    Name = e.Name,
                    Description = e.Description,
                    MaxPeople = e.MaxPeople,
                    DateFrom = e.DateFrom,
                    DateTo = e.DateTo,
                    Countries = e.CountryTrips.Select(e => new SomeSortOfCountry { Name = e.IdCountryNavigation.Name}).ToList(),
                    Clients = e.ClientTrips.Select(e => new SomeSortOfClient { FirstName = e.IdClientNavigation.FirstName, LastName = e.IdClientNavigation.LastName }).ToList()

            }).OrderByDescending(e => e.DateFrom)
              .ToListAsync();
        }
    }
}
