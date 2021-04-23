using System.Collections.Generic;
using System.Linq;
using NoTell_DAL.RepositoriesInterfaces;
using NoTell_Services.Interfaces;
using NoTell_Services.ViewModel;

namespace NoTell_Services.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public RoomVM GetRoomById(int roomId)
        {
            var room = _roomRepository.GetRoomById(roomId);

            if (room is null)
                return null;

            return new RoomVM
            {
                RoomNumber = room.RoomNumber,
                RoomId = roomId,
                NumberOfBedrooms = room.NumberOfBedrooms
            };
        }

        public IEnumerable<RoomVM> GetRoomsByBedrooms(int numberOfBedrooms)
        {
            var rooms = _roomRepository.GetRoomsByBedrooms(numberOfBedrooms).ToList();

            if (rooms.Any())
                return rooms.Select(x => new RoomVM
                {
                    RoomId = x.RoomId,
                    RoomNumber = x.RoomNumber,
                    NumberOfBedrooms = x.NumberOfBedrooms
                });

            return null;
        }
    }
}