using AutoMapper;
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
				option.MapFrom(src => src.CityImage!.FirstOrDefault(c => c.IsMain)!.Url);
			})
			.ReverseMap();
	}
}
