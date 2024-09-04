using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotelBooking.Dto.Model
{
    public class hotel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string? clientName { get; set; }
        [Required]
        public string? roomNumber { get; set; }
        [Required]
        public string? phoneNumber { get; set; }


    }
}