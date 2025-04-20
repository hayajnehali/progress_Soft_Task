using AutoMapper;
using BusinessCardInformation.Core.Models.Request;
using BusinessCardInformation.Core.Models.Response;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardInformation.Core.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BusinessCard, BusinessCardDTO>().ForMember(dest => dest.Photo, opt =>
                opt.MapFrom(src =>
                    src.Photo == null
                        ? null
                        : Convert.ToBase64String(src.Photo)
                ));
            CreateMap<BusinessCardDTO, BusinessCard>()
            .ForMember(dest => dest.Photo, opt => opt.MapFrom(src =>
                IsBase64String(src.Photo)
                    ? Convert.FromBase64String(src.Photo)
                    : null
            )); 

        }
        private static bool IsBase64String(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
                return false;

            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out _);
        }

    }
}
