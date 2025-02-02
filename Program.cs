
using Banking_system.AutoMapperProfiles;
using Banking_system.Data;
using Banking_system.Model;
using Banking_system.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Banking_system
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("cs")));


            builder.Services.AddIdentity<AppUser, Role>().
                AddEntityFrameworkStores<AppDbContext>();
              

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["JWT:issuer"],
                    ValidAudience = builder.Configuration["JWT:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(builder.Configuration["Jwt:secret"]))
                  //  RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"

                };
            }
                );

            builder.Services.AddScoped<IUnitOfWork, UnitOfWorkk>();

            builder.Services.AddAutoMapper(typeof(Program).Assembly);


          
            var app = builder.Build();

          

            app.UseCors("AllowAll");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
