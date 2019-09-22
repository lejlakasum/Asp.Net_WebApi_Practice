using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dot_NET_WebApiPractice.Data.Entities
{
    public class Game
    {
        public int GameId { get; set; }
        [Required]
        public DateTime GameDate { get; set; }

        [Required]
        public Team HomeTeam { get; set; }

        [Required]
        public Team GuestTeam { get; set; }

        [Required]
        public Competition Competition { get; set; }
    }
}