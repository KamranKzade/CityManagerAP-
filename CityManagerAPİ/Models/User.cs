namespace CityManagerAPİ.Models;

public class User
{
    public int Id { get; set; }
    public string? Username { get; set; }
    
    // Parolun hashlanmis variantini saxlayiriq ki ,sonra password yazanda onlari hashliyib muqayise edek.
    public byte[]? PasswordHash { get; set; }
    
    // Inst, Fb parollar eyni olsada, onlarin hashlari eyni olana deyil, yeni 1 yerden parolu ogurlasalar,gedib basqa yerde yazsa uygun gelmiyecek parol.Bu sebebden PasswordSaltdan istifade edilir.
    public byte[]? PasswordSalt { get; set; }

    public List<City>? Cities { get; set; }

}
