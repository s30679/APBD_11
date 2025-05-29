using APBD_11.DAL;
using APBD_11.Services;
using Microsoft.EntityFrameworkCore;

namespace APBD_11;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.AddDbContext<HospitalDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseSqlServer(connectionString);
        });
        builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.Run();
    }
}