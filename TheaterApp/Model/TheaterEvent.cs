using System;
using System.ComponentModel.DataAnnotations;

namespace TheaterApp.Model
{
    public class TheaterEvent
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}