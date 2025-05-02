using AutoMapper;
using SmartLedger.Application.DTOs;
using SmartLedger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartLedger.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>()
                .ForMember(dest => dest.AccountIdAndName,
                    opt => opt.MapFrom(src => $"{src.Id} - {src.AccountName}"));
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.CategoryIdAndName,
                    opt => opt.MapFrom(src => $"{src.Id} - {src.CategoryName}"));
        }
    }
}
