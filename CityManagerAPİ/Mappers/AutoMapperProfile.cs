﻿using AutoMapper;
using CityManagerAPİ.Dtos;
using CityManagerAPİ.Models;


namespace CityManagerAPİ.Mappers;


public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
		CreateMap<City, CityForListDto>()
			   .ForMember(dest => dest.PhotoUrl, option =>
			   {
				   option.MapFrom(src => src.CityImages.FirstOrDefault(c => c.IsMain).Url);
			   })
			   .ReverseMap();

		CreateMap<City, CityDto>().ReverseMap();
		CreateMap<City, CityDetailDto>().ReverseMap();
		CreateMap<CityImage, CityImageDto>().ReverseMap();
	}
}
