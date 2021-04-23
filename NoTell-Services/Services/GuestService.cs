using NoTell_DAL.Entities;
using NoTell_DAL.RepositoriesInterfaces;
using NoTell_Services.Interfaces;

namespace NoTell_Services.Services
{
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepository;

        public GuestService(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public int AddGuest(string name, string lastName, string phone) => _guestRepository.AddGuest(name, lastName, phone);

        public Guest GetGuestById(int guestId) => _guestRepository.GetGuestById(guestId);
    }
}