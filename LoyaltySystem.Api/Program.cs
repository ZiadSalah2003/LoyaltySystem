
using Hangfire;
using LoyaltySystem.Api.Authentication;
using LoyaltySystem.Api.Data;
using LoyaltySystem.Api.Repositories;
using LoyaltySystem.Api.Services;
using MailKit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Hangfire.PostgreSql;

namespace LoyaltySystem.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{

			var builder = WebApplication.CreateBuilder(args);
			var connectionStirng = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("No connection string was found");
			builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionStirng));
			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));

			var jwtSettings = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();


			builder.Services.AddHangfire(configuration => configuration
			.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
			.UseSimpleAssemblyNameTypeSerializer()
			.UseRecommendedSerializerSettings()
			.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

			builder.Services.AddHangfireServer();

			builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));


			builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
			builder.Services.AddScoped<IAdminService, AdminService>();
			builder.Services.AddScoped<IEmailSender, EmailService>();
			builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
			builder.Services.AddScoped<ICustomerService, CustomerService>();
			builder.Services.AddScoped<IAuthService, AuthService>();
			builder.Services.AddSingleton<IJwtProvider, JwtProvider>();

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(o =>
				{
					o.SaveToken = true;
					o.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
						ValidIssuer = jwtSettings?.Issuer,
						ValidAudience = jwtSettings?.Audience
					};
				});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
				
			}

			app.UseHttpsRedirection();

			app.UseHangfireDashboard("/jobs");

			app.UseAuthorization();

			app.MapControllers();
			app.Run();
		}
	}
}
