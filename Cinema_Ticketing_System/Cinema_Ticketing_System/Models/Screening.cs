using System;


namespace Cinema_Ticketing_System.Models
{
    public class Screening
    {
        public int Id { get; set; }

        public Film Film {get; set; }

        public int FilmId{ get; set; }

        public Screen Screen { get; set; }

        public int ScreenId { get; set; }

        public DateTime DateAndTime { get; set; }


    }
}