
using System.Text.Json.Serialization;
using MarkGrader.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


ServicesConfig.Services = builder.Services;
ServicesConfig.AddDIs();
ServicesConfig.AddCors();
ServicesConfig.AddJWTAuthentication(builder.Configuration["JWT:SecretKey"].ToString());

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
