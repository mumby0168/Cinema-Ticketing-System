using System;



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

        public string Name { get; set; }

        public Genre Genre {get; set; }
    }
}