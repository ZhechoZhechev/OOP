
namespace BookingApp.Repositories
{
    using System.Collections.Generic;

    using Contracts;
    using Models.Rooms.Contracts;
    public class RoomRepository : IRepository<IRoom>
    {
        private List<IRoom> rooms;

        public RoomRepository()
        {
            this.rooms = new List<IRoom>();
        }
        public void AddNew(IRoom model)
        {
            this.rooms.Add(model);
        }

        public IReadOnlyCollection<IRoom> All()
        {
            return this.rooms.AsReadOnly();
        }

        public IRoom Select(string criteria)
        {
            return this.rooms.Find(x => x.GetType().Name == criteria);
        }
    }
}
