using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dot_NET_WebApiPractice.Data.Entities
{
    public class Team
    {
        public int TeamId { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Moniker { get; set; }

        [StringLength(80, MinimumLength = 5)]
        public string Hall { get; set; }

        [Required]
        public DateTime EstablishmentDate { get; set; }

        public ICollection<Player> Players { get; set; }
        public ICollection<Game> HomeGames { get; set; }
        public ICollection<Game> GuestGames { get; set; }

    }
}