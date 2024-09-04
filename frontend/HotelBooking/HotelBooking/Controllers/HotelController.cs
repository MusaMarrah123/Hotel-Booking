using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Dto;
using HotelBooking.Dto.Model;

namespace HotelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public HotelController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotel()
        {
            var hotels = await _context.hotels.ToListAsync();
            return Ok(hotels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleHotel(int id)
        {
            var school = await _context.hotels.FindAsync(id);
            if (school is null)
                return BadRequest("hotel Not Found");
            return Ok(school);
        }
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult<hotel>> AddHotel(hotelDto hoteldto)
        {
           
            var newCharacter = new hotel
            {
                ID = hoteldto.id,
                clientName = hoteldto.clientName,
                roomNumber = hoteldto.roomNumber,
                phoneNumber = hoteldto.phoneNumber,
            };
            _context.hotels.Add(newCharacter);
            await _context.SaveChangesAsync();
            return Ok(new Response { Data = "record added successfully", Status = "Success", Message = "hotel Updated Successfully" });

        }
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateSchool([FromBody]hotel hos)
        {
            var existingHotel = await _context.hotels.FindAsync(hos.ID);
            if (existingHotel is null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Hotel Room  not found." });

            existingHotel.clientName = hos.clientName;
            existingHotel.roomNumber = hos.roomNumber;
            existingHotel.phoneNumber= hos.phoneNumber;

            await _context.SaveChangesAsync();
            return Ok(new Response { Data = existingHotel.ToString(), Status = "Success", Message = "hotel Updated Successfully"});

        }
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var school = await _context.hotels.FindAsync(id);
            if (school is null)
                return BadRequest("hotel Not Found");

            _context.hotels.Remove(school);
            await _context.SaveChangesAsync();
            return Ok(new Response { Data = GetAllHotel().ToString(), Status = "Success", Message = "hotel Updated Successfully" });

        }


    }
}
