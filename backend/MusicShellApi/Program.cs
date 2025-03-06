using System.Reflection;
using Microsoft.OpenApi.Models;
using MusicShellApi.Mediators.Implementation;
using MusicShellApi.Mediators.Interfaces;
using MusicShellApi.Services.Config;
using MusicShellApi.Services.Factories;
using MusicShellApi.Services.Implementation;
using MusicShellApi.Services.Interfaces;
using MusicShellApi.Services.Providers;
using MusicShellApi.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    // Add more Swagger configurations if needed
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Music Shell API",
        Version = "v1",
        Description = "An API for managing and playing music from various providers.",
        Contact = new OpenApiContact
        {
            Name = "Asad W.",
            Url = new Uri("https://github.com/asadw1/")
        }
    });
});

builder.Services.AddSingleton<IFileSystem, FileSystem>();

// Read configuration settings
var configuration = builder.Configuration;
var musicProvider = configuration["MusicProvider"] ?? "Local";

// Register the appropriate factory based on the configuration
builder.Services.AddSingleton<IMusicProviderFactory>(provider =>
{
    var fileSystem = provider.GetRequiredService<IFileSystem>();
    var musicFilesPath = configuration["MusicFilesPath"] ?? "../MusicFiles";

    return musicProvider switch
    {
        "Spotify" => new SpotifyMusicProviderFactory(configuration.GetSection("SpotifyConfig").Get<SpotifyConfig>() ?? new SpotifyConfig { ClientId = "", ClientSecret = "", RedirectUri = "" }),
        "Pandora" => new PandoraMusicProviderFactory(configuration.GetSection("PandoraConfig").Get<PandoraConfig>() ?? new PandoraConfig { ApiKey = "", Secret = "" }),
        _ => new LocalMusicProviderFactory(fileSystem, musicFilesPath),
    };
});

builder.Services.AddSingleton<IMusicService, MusicService>();
builder.Services.AddSingleton<IMusicMediator, MusicMediator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
