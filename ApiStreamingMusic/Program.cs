using ApiStreamingMusic.Application.Interfaces;
using ApiStreamingMusic.Application.Persistence;
using ApiStreamingMusic.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMusicService, MusicService>();
builder.Services.AddSingleton<AuthService, AuthService>();
builder.Services.AddScoped<IMusicRepository, MusicRepository>();
builder.Services.AddSingleton<DapperContext, DapperContext>();
var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabaseKey = builder.Configuration["Supabase:ApiKey"];
var supabaseClient = new Supabase.Client(supabaseUrl, supabaseKey);

builder.Services.AddSingleton(supabaseClient);

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
