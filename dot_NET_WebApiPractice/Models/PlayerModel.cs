using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dot_NET_WebApiPractice.Models
{
    public class PlayerModel
    {
        public int PlayerId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [StringLength(20, MinimumLength = 3)]
        public string Position { get; set; }

    }
}