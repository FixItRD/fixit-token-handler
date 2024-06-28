using fixit_token_handler.Services;
using fixit_token_handler.Services.Templates;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace fixit_token_handler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Configuracion de JWT
            var JwtConfiguration = new TokenConfiguration(builder.Configuration["JwtSettings:Issuer"], builder.Configuration["JwtSettings:Audience"], new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])));


            // Configuraciones
            builder.Services.ConfigureCORS("_MyAllowSpecifiOrigins");
            builder.Services.ConfigureJWT(builder.Configuration);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Adding dependencies
            builder.Services.AddSingleton(JwtConfiguration);
            builder.Services.AddSingleton<IServiceHandler, ServiceHandler>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
