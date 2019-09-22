using AutoMapper;
using dot_NET_WebApiPractice.Data.Entities;
using dot_NET_WebApiPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dot_NET_WebApiPractice.Data
{
    public class PlayerMappingProfile : Profile
    {
        public PlayerMappingProfile()
        {
            CreateMap<Player, PlayerModel>()
                .ReverseMap()
                .ForMember(ply=>ply.Team, opt => opt.Ignore());
        }
    }
}