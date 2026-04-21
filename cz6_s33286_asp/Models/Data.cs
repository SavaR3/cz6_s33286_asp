using cz6_s33286_asp.Models;

namespace cz6_s33286_asp.Data
{
    public static class Data
    {
        public static List<Room> Rooms { get; set; } = new List<Room>
        {
            new Room { Id = 1, Name = "Room 101", BuildingCode = "A", Floor = 1, Capacity = 20, HasProjector = true, IsActive = true },
            new Room { Id = 2, Name = "Room 102", BuildingCode = "A", Floor = 1, Capacity = 30, HasProjector = false, IsActive = true },
            new Room { Id = 3, Name = "Aula 1", BuildingCode = "B", Floor = 0, Capacity = 150, HasProjector = true, IsActive = true },
            new Room { Id = 4, Name = "Lab 1", BuildingCode = "C", Floor = 2, Capacity = 15, HasProjector = true, IsActive = true },
            new Room { Id = 5, Name = "Maintenance", BuildingCode = "C", Floor = -1, Capacity = 2, HasProjector = false, IsActive = false }
        };

        public static List<Reservation> Reservations { get; set; } = new List<Reservation>
        {
            new Reservation { Id = 1, RoomId = 1, OrganizerName = "Andrzej Novak", Topic = "Math", Date = new DateTime(2026, 5, 10), StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(9, 30, 0), Status = "confirmed" },
            new Reservation { Id = 2, RoomId = 1, OrganizerName = "Mike Smith", Topic = "Physics", Date = new DateTime(2026, 5, 10), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(11, 30, 0), Status = "confirmed" },
            new Reservation { Id = 3, RoomId = 3, OrganizerName = "Adam Tomaszewski", Topic = "Computer Science", Date = new DateTime(2026, 5, 11), StartTime = new TimeSpan(12, 0, 0), EndTime = new TimeSpan(14, 0, 0), Status = "planned" },
            new Reservation { Id = 4, RoomId = 4, OrganizerName = "Jan Kowalski", Topic = "Programming", Date = new DateTime(2026, 5, 12), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(15, 0, 0), Status = "confirmed" },
            new Reservation { Id = 5, RoomId = 2, OrganizerName = "Viktoria Lewandowska", Topic = "Algorithms", Date = new DateTime(2026, 5, 12), StartTime = new TimeSpan(16, 0, 0), EndTime = new TimeSpan(17, 30, 0), Status = "cancelled" },
            new Reservation { Id = 6, RoomId = 1, OrganizerName = "John Doe", Topic = "Math II", Date = new DateTime(2026, 5, 13), StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0), Status = "planned" }
        };
    }
}