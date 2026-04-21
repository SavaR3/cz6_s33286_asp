using cz6_s33286_asp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cz6_s33286_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private List<Room> _rooms = Data.Data.Rooms;
        private List<Reservation> _reservations = Data.Data.Reservations;

        [HttpGet]
        public IActionResult GetRooms([FromQuery] int? minCapacity, [FromQuery] bool? hasProjector, [FromQuery] bool? activeOnly)
        {
            IEnumerable<Room> result = _rooms;

            if (minCapacity != null) result = result.Where(r => r.Capacity >= minCapacity);
            if (hasProjector != null) result = result.Where(r => r.HasProjector == hasProjector);
            if (activeOnly != null) result = result.Where(r => r.IsActive == activeOnly);

            return Ok(result.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetRoom(int id)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound();
            return Ok(room);
        }

        [HttpGet("building/{buildingCode}")]
        public IActionResult GetRoomsByBuilding(string buildingCode)
        {
            var result = _rooms.Where(r => r.BuildingCode == buildingCode).ToList();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateRoom([FromBody] Room room)
        {
            room.Id = _rooms.Max(r => r.Id) + 1;
            _rooms.Add(room);
            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, [FromBody] Room updatedRoom)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound();

            room.Name = updatedRoom.Name;
            room.BuildingCode = updatedRoom.BuildingCode;
            room.Floor = updatedRoom.Floor;
            room.Capacity = updatedRoom.Capacity;
            room.HasProjector = updatedRoom.HasProjector;
            room.IsActive = updatedRoom.IsActive;

            return Ok(room);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound();

            foreach (var res in _reservations)
            {
                if (res.RoomId == id && res.Date >= DateTime.Today)
                {
                    return Conflict();
                }
            }

            _rooms.Remove(room);
            return NoContent();
        }
    }
}
