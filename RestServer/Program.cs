using Microsoft.EntityFrameworkCore;
using RestServer;
using RestServer.Data;
using RestServer.Data.DAOImplementation;
using RestServer.Data.DAOInterfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMovieDao, MovieDao>();
builder.Services.AddScoped<IAccountDao, AccountDao>();
builder.Services.AddScoped<IPeopleDao, PeopleDao>();
builder.Services.AddScoped<ICommentDao, CommentDao>();
builder.Services.AddScoped<IWatchListDao, WatchListDao>();
builder.Services.AddScoped<IStarsDao, StarsDao>();

//App settings
AppSettings? appSettings = builder.Configuration.Get<AppSettings>(options =>
{
    options.BindNonPublicProperties = true;
});

if (appSettings == null)
{
    throw new ArgumentException($"{nameof(AppSettings)} was not loaded.");
}

//DB
builder.Services.AddDbContext<Context>(options =>
{
    //Later can be stored in appsettings
    options.UseNpgsql(AppSettings.DATABASE_CONNECTION_STRING);
    options.EnableSensitiveDataLogging();
});


//->BUILD
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();