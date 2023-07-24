using CityManagerAPİ.Data;
using CityManagerAPİ.Models;
using CityManagerAPİ.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CityManagerAPİ.Repository.Concrete
{
	public class CityImageRepository : ICityImageRepository
	{
		private readonly DataContext _dataContext;

		public CityImageRepository(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public async void Add(CityImage entity)
		{
			_dataContext.CityImages.Add(entity);
			await _dataContext.SaveChangesAsync();
		}

		public async void Delete(CityImage entity)
		{
			_dataContext.CityImages.Remove(entity);
			await _dataContext.SaveChangesAsync();
		}

		public IEnumerable<CityImage> GetAll()
		{
			return _dataContext.CityImages;
		}

		public CityImage GetByID(int id)
		{
			return _dataContext.CityImages.FirstOrDefault(x => x.Id == id)!;
		}

		public async void Update(CityImage entity)
		{
			_dataContext.Entry(entity).State = EntityState.Modified;
			await _dataContext.SaveChangesAsync();
		}

		public List<CityImage> GetPhotosByCityId(int cityId)
		{
			var photos = _dataContext
			   .CityImages
			   .Where(c => c.CityId == cityId)
			   .ToList();
			return photos;
		}
	}
}
