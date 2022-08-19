using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ecommerce.Models;
using Ecommerce.ViewModel;

namespace Ecommerce.AutoMapper
{
    public static class AutoMapping
    {
        public static IMapper Mapper;
        public static void Init()
        {
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<Category, CategoryVM>().
                      ForMember(dest => dest.ID, src => src.Ignore()).
                      ReverseMap();
                cfg.CreateMap<Brand, BrandVM>().
                     ForMember(dest => dest.ID, src => src.Ignore()).
                     ReverseMap();
                cfg.CreateMap<Product, ProductVM>().
                     ForMember(dest => dest.ID, src => src.Ignore()).
                     ReverseMap();
            });
            Mapper = config.CreateMapper();
        }

    }
}