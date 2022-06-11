using System;
using System.Collections.Generic;

#nullable disable

namespace cw5.Models
{
    public partial class Client
    {
        public Client()
        {
            ClientTrips = new HashSet<ClientTrip>();
        }

        public Client(string firstName, string lastName, string email, string telephone, string pesel)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Telephone = telephone;
            Pesel = pesel;
        }

        public int IdClient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Pesel { get; set; }

        public virtual ICollection<ClientTrip> ClientTrips { get; set; }
    }
}
