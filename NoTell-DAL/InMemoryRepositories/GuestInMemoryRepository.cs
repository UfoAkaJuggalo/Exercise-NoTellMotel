using System.Collections.Generic;
using System.Linq;
using NoTell_DAL.Entities;
using NoTell_DAL.RepositoriesInterfaces;

namespace NoTell_DAL.InMemoryRepositories
{
    public class GuestInMemoryRepository :IGuestRepository
    {
        private readonly List<Guest> _guests;
        private int _idCounter;

        public GuestInMemoryRepository()
        {
            _guests = new List<Guest>();
            _idCounter = 0;
        }

        public int AddGuest(string name, string lastName, string phone)
        {
            _guests.Add(new Guest
            {
                Name = name,
                LastName = lastName,
                Phone = phone,
                GuestId = ++_idCounter
            });

            return _idCounter;
        }

        public Guest GetGuestById(int guestId)
        {
            return _guests.FirstOrDefault(f => f.GuestId == guestId);
        }
    }
}