using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace dot_NET_WebApiPractice.Data.Entities
{
    public class Competition
    {
        public int CompetitionId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }

        public static implicit operator Task<object>(Competition v)
        {
            throw new NotImplementedException();
        }
    }
}