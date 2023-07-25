using CityManagerAPİ.Models;

namespace CityManagerAPİ.Data;

public interface IAuthRepository 
{
	Task<User> RegisterAsync(User user, string password);
	Task<User> LoginAsync(string username, string password);
	Task<bool> UserExists(string username);
}
