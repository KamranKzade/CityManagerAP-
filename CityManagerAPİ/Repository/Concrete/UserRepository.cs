using CityManagerAPİ.Data;
using CityManagerAPİ.Models;
using Microsoft.EntityFrameworkCore;
using CityManagerAPİ.Repository.Abstract;

namespace CityManagerAPİ.Repository.Concrete;

public class UserRepository : IUserRepository
{
	private readonly DataContext _context;

	public UserRepository(DataContext dataContext)
	{
		_context = dataContext;
	}

	public async void Add(User entity)
	{
		_context.Users.Add(entity);
		await _context.SaveChangesAsync();
	}

	public async void Delete(User entity)
	{
		_context.Users.Remove(entity);
		await _context.SaveChangesAsync();
	}

	public IEnumerable<User> GetAll()
	{
		return _context.Users;
	}

	public User GetByID(int id)
	{
		return  _context.Users.FirstOrDefault(x => x.Id == id)!;
	}

	public async void Update(User entity)
	{
		_context.Entry(entity).State = EntityState.Modified;
		await _context.SaveChangesAsync();
	}

}
