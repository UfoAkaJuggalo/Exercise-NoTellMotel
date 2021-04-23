using System.Collections.Generic;
using NoTell_Services.ViewModel;

namespace NoTell_Services.Interfaces
{
    public interface IRoomService
    {
        RoomVM GetRoomById(int roomId);
        IEnumerable<RoomVM> GetRoomsByBedrooms(int numberOfBedrooms);
    }
}