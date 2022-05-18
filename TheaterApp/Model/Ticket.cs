using System.ComponentModel.DataAnnotations;

namespace TheaterApp.Model
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int SitNumber { get; set; }
        [Required]
        public int RowNumber { get; set; }
        [Required]
        public int EventId { get; set; }
        public int Cost { get; set; }
        public TicketState State { get; set; }
    }
}