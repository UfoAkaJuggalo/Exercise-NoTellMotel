using System.Collections.Generic;
using System.Linq;
using NoTell_DAL.Entities;

namespace NoTell_DAL.RepositoriesInterfaces
{
    public interface IRoomRepository
    {
        int AddRoom(int roomNumber, int bedrooms);
        Room GetRoomById(int roomId);
        IEnumerable<Room> GetRoomsByBedrooms(int numberOfBedrooms);
        IEnumerable<Room> GetAllRooms();
    }
}