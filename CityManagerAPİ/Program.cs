using System.Text;
using CityManagerAPİ.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using CityManagerAPİ.Repository.Concrete;
using CityManagerAPİ.Repository.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Loopu saxlamaq ucun istifade olunur.
builder.Services.AddControllers()
				.AddJsonOptions(opt =>
				{
					opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
				});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
	builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


var conn = builder.Configuration.GetConnectionString("myConn");

builder.Services.AddDbContext<DataContext>(opt =>
{
	opt.UseSqlServer(conn);
});

var word = builder.Configuration.GetSection("AppSettings:Token").Value;
var key = Encoding.ASCII.GetBytes(word);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(key),
			ValidateIssuer = false,
			ValidateAudience = false
		};
	});

builder.Services.AddScoped<IAppRepository, AppRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICityImageRepository, CityImageRepository>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseCors("corsapp");
app.UseAuthorization();

app.MapControllers();

app.Run();
