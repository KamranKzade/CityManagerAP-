﻿namespace CityManagerAPİ.Models;

public class City
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<CityImage>? CityImage { get; set; }
}
