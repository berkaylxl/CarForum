﻿using AutoMapper;
using CarForum.Api.Domain.Models;
using CarForum.Common.Models.Queries;
using CarForum.Common.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Api.Application.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserViewModel>().ReverseMap();
            CreateMap<CreateUserCommand, User>();
            CreateMap<UpdateUserCommand, User>();
            CreateMap<CreateEntryCommand, Entry>().ReverseMap();
            CreateMap<CreateEntryCommentCommand, EntryComment>().ReverseMap();
        }
    }
}
