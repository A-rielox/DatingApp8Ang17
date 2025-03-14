﻿using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        //           -------->
        CreateMap<AppUser, MemberDto>()
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
                src.Photos.FirstOrDefault(x => x.IsMain)!.Url))
        .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
            src.DateOfBirth.CalculateAge()));
        // puedo ocupar !. xq automapper en lugar de tirar excepcion va a mandar un null, xlo q no hay peligro

        CreateMap<Photo, PhotoDto>();
        CreateMap<MemberUpdateDto, AppUser>();
        //CreateMap<RegisterDto, AppUser>();

        //CreateMap<Message, MessageDto>()
        //    .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src => 
        //        src.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
        //    .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(src =>
        //        src.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));
    }
}


// agrego services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly); en ApplicationServiceExtensions
