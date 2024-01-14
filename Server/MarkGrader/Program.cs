using MarkGrader.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

ServiceConfig.Services = builder.Services;
ServiceConfig.AddIdentityUser();
ServiceConfig.AddDIs();
ServiceConfig.AddCors();
ServiceConfig.AddSwagger();
ServiceConfig.AddJWTAuthentication(builder.Configuration["JWT:SecretKey"]!);
ServiceConfig.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
