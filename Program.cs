using EmployeeApp.DAL.Configurations;
using EmployeeApp.DAL.EmployeeDBContext;
using EmployeeApp.DAL.IRepositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logFolderPath = Path.Combine("D:", "Microservices_Samples\\EmployeeAppService\\LogFiles"); // ? You can customize the path

// Create folder if not exists
if (!Directory.Exists(logFolderPath))
{
    Directory.CreateDirectory(logFolderPath);
}

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
        Path.Combine(logFolderPath, "log-.txt"), // ? Save in your folder
        rollingInterval: RollingInterval.Day,    // ? New file every day
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
    )
    .CreateLogger();

// Add Serilog to ASP.NET Core logging
builder.Host.UseSerilog();

builder.Services.AddDbContext<EmployeeContext>(
    opts => opts.UseSqlServer(builder.Configuration["ConnectionStrings:CRUDEmployee"])
    );

builder.Services.AddScoped<IEmployee , EmployeeConfigurations>();

builder.Services.AddControllers();
    //.AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy= null);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
