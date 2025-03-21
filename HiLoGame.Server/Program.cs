
using HiLoGame.Application.Repositories.Interfaces;
using HiLoGame.Application.Services;
using HiLoGame.Application.Services.Interfaces;
using HiLoGame.Infrastructure;
using HiLoGame.Infrastructure.Hubs;
using HiLoGame.Infrastructure.Repositories;
using HiLoGame.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace HiLoGame.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<HiLoGameContext>(options =>
                options.UseInMemoryDatabase("HiLoGameDB"));

            builder.Services.AddScoped<IGameRepository, GameRepository>();
            builder.Services.AddSingleton<INotifyService, NotifyService>();
            builder.Services.AddScoped<IGameService, GameService>();

            builder.Services.AddSignalR();

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.MapHub<GameHub>("/api/gameHub");

            app.Run();
        }
    }
}
