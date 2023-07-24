﻿namespace CityManagerAPİ.Dtos;


public class CityImageDto
{
	public int Id { get; set; }
	public int CityId { get; set; }
	public string? Url { get; set; }
	public string? Description { get; set; }
	public DateTime DateAdded { get; set; }
	public bool IsMain { get; set; }
}
