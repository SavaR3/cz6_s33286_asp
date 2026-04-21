using cz6_s33286_asp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cz6_s33286_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private List<Room> _rooms = Data.Data.Rooms;
        private List<Reservation> _reservations = Data.Data.Reservations;

        [HttpGet]
        public IActionResult GetReservations([FromQuery] DateTime? date, [FromQuery] string? status, [FromQuery] int? roomId)
        {
            IEnumerable<Reservation> result = _reservations;

            if (date != null) result = result.Where(r => r.Date.Date == date.Value.Date);
            if (status != null) result = result.Where(r => r.Status == status);
            if (roomId != null) result = result.Where(r => r.RoomId == roomId);

            return Ok(result.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetReservation(int id)
        {
            var reservation = _reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null) return NotFound();
            return Ok(reservation);
        }

        [HttpPost]
        public IActionResult CreateReservation([FromBody] Reservation reservation)
        {
            if (reservation.EndTime <= reservation.StartTime) return BadRequest();

            var room = _rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
            if (room == null) return BadRequest();
            if (room.IsActive == false) return BadRequest();

            foreach (var r in _reservations)
            {
                if (r.RoomId == reservation.RoomId && r.Date.Date == reservation.Date.Date)
                {
                    if (reservation.StartTime < r.EndTime && reservation.EndTime > r.StartTime)
                    {
                        return Conflict();
                    }
                }
            }

            reservation.Id = _reservations.Max(r => r.Id) + 1;
            _reservations.Add(reservation);
            return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReservation(int id, [FromBody] Reservation updatedReservation)
        {
            var reservation = _reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null) return NotFound();

            if (updatedReservation.EndTime <= updatedReservation.StartTime) return BadRequest();

            var room = _rooms.FirstOrDefault(r => r.Id == updatedReservation.RoomId);
            if (room == null) return BadRequest();
            if (room.IsActive == false) return BadRequest();

            foreach (var r in _reservations)
            {
                if (r.Id != id && r.RoomId == updatedReservation.RoomId && r.Date.Date == updatedReservation.Date.Date)
                {
                    if (updatedReservation.StartTime < r.EndTime && updatedReservation.EndTime > r.StartTime)
                    {
                        return Conflict();
                    }
                }
            }

            reservation.RoomId = updatedReservation.RoomId;
            reservation.OrganizerName = updatedReservation.OrganizerName;
            reservation.Topic = updatedReservation.Topic;
            reservation.Date = updatedReservation.Date;
            reservation.StartTime = updatedReservation.StartTime;
            reservation.EndTime = updatedReservation.EndTime;
            reservation.Status = updatedReservation.Status;

            return Ok(reservation);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            var reservation = _reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null) return NotFound();

            _reservations.Remove(reservation);
            return NoContent();
        }
    }
}