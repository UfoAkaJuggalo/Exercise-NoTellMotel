using System.Collections.Generic;
using System.Linq;
using NoTell_DAL.Entities;
using NoTell_DAL.RepositoriesInterfaces;

namespace NoTell_DAL.InMemoryRepositories
{
    public class RoomInMemoryRepository : IRoomRepository
    {
        private readonly List<Room> _rooms;
        private int _idCounter;

        public RoomInMemoryRepository()
        {
            _rooms = new List<Room>();
            _idCounter = 0;

            for (int i = 1; i < 5; i++)
                AddRoom(i, 1);

            for (int i = 5; i < 8; i++)
                AddRoom(i, 2);

            for (int i = 8; i < 11; i++)
                AddRoom(i, 3);
        }
        
        public int AddRoom(int roomNumber, int bedrooms)
        {
            _rooms.Add(new Room
            {
                RoomNumber = roomNumber,
                NumberOfBedrooms = bedrooms,
                RoomId = _idCounter++
            });

            return _idCounter;
        }

        public Room GetRoomById(int roomId) => _rooms.FirstOrDefault(f => f.RoomId == roomId);

        public IEnumerable<Room> GetRoomsByBedrooms(int numberOfBedrooms) => _rooms.Where(x => x.NumberOfBedrooms == numberOfBedrooms);
    }
}