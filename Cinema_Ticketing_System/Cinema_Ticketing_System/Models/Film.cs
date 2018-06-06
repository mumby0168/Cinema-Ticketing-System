using System;
using System.ComponentModel.DataAnnotations;


namespace Cinema_Ticketing_System.Models
{
    public enum Genre
    {
        Action,
        Drama,
        Comedy,
        Thriller,
        Horror
    }
    

    public class Film
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public Genre Genre {get; set; }
    }
}