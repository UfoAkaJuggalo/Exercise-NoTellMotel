using NoTell_DAL.Entities;

namespace NoTell_DAL.RepositoriesInterfaces
{
    public interface IGuestRepository
    {
        int AddGuest(string name, string lastName, string phone);
        Guest GetGuestById(int guestId);
        Guest getGuestByNamePhone(string name, string lastName, string phone);
    }
}