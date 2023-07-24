using CityManagerAPİ.Data;
using CityManagerAPİ.Models;
using Microsoft.EntityFrameworkCore;
using CityManagerAPİ.Repository.Abstract;

namespace CityManagerAPİ.Repository.Concrete;

public class CityRepository : ICityRepository
{
	private readonly DataContext _dataContext;

	public CityRepository(DataContext dataContext)
	{
		_dataContext = dataContext;
	}

	public async void Add(City entity)
	{
		_dataContext.Cities.Add(entity);
		await _dataContext.SaveChangesAsync();
	}

	public async void Delete(City entity)
	{
		_dataContext.Cities.Remove(entity);
		await _dataContext.SaveChangesAsync();
	}

	public IEnumerable<City> GetAll()
	{
		return _dataContext.Cities;
	}

	public City GetByID(int id)
	{
		return _dataContext.Cities.FirstOrDefault(x => x.Id == id)!;
	}

	public async void Update(City entity)
	{
		_dataContext.Entry(entity).State = EntityState.Modified;
		await _dataContext.SaveChangesAsync();
	}
}
