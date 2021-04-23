using NoTell_DAL.Entities;

namespace NoTell_Services.Interfaces
{
    public interface IGuestService
    {
        int AddGuest(string name, string lastName, string phone);
        Guest GetGuestById(int guestId);
    }
}