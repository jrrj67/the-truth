﻿using JogandoBack.API.Data.Entities;
using JogandoBack.API.Data.Requests;
using JogandoBack.API.Data.Responses;
using AutoMapper;

namespace JogandoBack.API.Data.Profiles
{
    public class RolesProfile : Profile
    {
        public RolesProfile()
        {
            CreateMap<RolesRequest, RolesEntity>();

            CreateMap<RolesEntity, RolesResponse>();
        }
    }
}
