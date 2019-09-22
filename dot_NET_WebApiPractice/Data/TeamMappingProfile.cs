using AutoMapper;
using dot_NET_WebApiPractice.Data.Entities;
using dot_NET_WebApiPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dot_NET_WebApiPractice.Data
{
    public class TeamMappingProfile : Profile
    {
        public TeamMappingProfile()
        {
            CreateMap<Team, TeamModel>().ReverseMap();
        }
    }
}