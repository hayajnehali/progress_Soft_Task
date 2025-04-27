using AutoMapper;
using BusinessCardInformation.Core.Models.Request;
using BusinessCardInformation.Core.Models.Response;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
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
                        : ToBase64String(src.Photo)
                )); 
            CreateMap<BusinessCardDTO, BusinessCard>()
            .ForMember(dest => dest.Photo, opt => opt.MapFrom(src =>
                IsBase64String(src.Photo)
                    ? FromBase64String(src.Photo)
                    : null
            )); 

        }
        private static bool IsBase64String(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
                return false;
            return true;
        }
        
        private static byte[] FromBase64String(string str)
        { 
            byte[] byt = new byte[str.Length]; 
            for (int i = 0; i < str.Length; i++)
            {
                byt[i] = Convert.ToByte(str[i]);
            }
            return byt;
        }
        
        private static string ToBase64String(byte[] str)
        {
            string byt = System.Text.Encoding.UTF8.GetString(str);
            return byt;
        }


    }
}
