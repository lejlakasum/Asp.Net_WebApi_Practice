using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dot_NET_WebApiPractice.Data.Entities
{
    public class Player
    {
        public int PlayerId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [StringLength(20,MinimumLength = 3)]
        public string Position { get; set; }

        [Range(0,int.MaxValue)]
        public double Salary { get; set; }

        //[Required]
        public Team Team { get; set; }
    }
}