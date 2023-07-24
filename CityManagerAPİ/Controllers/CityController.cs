using AutoMapper;
using CityManagerAPİ.Data;
using CityManagerAPİ.Dtos;
using CityManagerAPİ.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityManagerAPİ.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CityController : ControllerBase
{
	private IAppRepository _appRepository;
	private IMapper _mapper;

	public CityController(IAppRepository appRepository, IMapper mapper)
	{
		_appRepository = appRepository;
		_mapper = mapper;
	}


	[HttpGet("{id?}")]
	public IActionResult GetCities(int id = 1)
	{
		//	var cities = _appRepository.GetCities(id)
		//		.Select(c =>
		//		{
		//			return new CityForListDto
		//			{
		//				Id = c.Id,
		//				Name = c.Name,
		//				Description = c.Description,
		//				PhotoUrl = c.CityImage!.FirstOrDefault(p => p.IsMain)!.Url
		//			};
		//		});

		//	return Ok(cities);

		var result = _appRepository.GetCities(id);
		var dtos = _mapper.Map<List<CityForListDto>>(result);
		return Ok(dtos);
	}

	[HttpPost]
	[Route("add")]
	public IActionResult Add([FromBody] CityDto dto)
	{
		try
		{
			var item = _mapper.Map<City>(dto);
			_appRepository.Add(item);
			var result = _appRepository.SaveAll();
			if (result)
			{
				return Ok(item);
			}
			return BadRequest();

		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("Detail")]
	public IActionResult GetCityById(int id)
	{
		var city = _appRepository.GetCityById(id);
		var cityToReturn = _mapper.Map<CityDetailDto>(city);
		return Ok(cityToReturn);
	}

	[HttpGet("Photos/{cityId}")]
	public IActionResult GetPhotosByCityId(int cityId)
	{
		var photos = _appRepository.GetPhotosByCityId(cityId);
		var dtos = _mapper.Map<List<CityImageDto>>(photos);
		return Ok(dtos);
	}
}
