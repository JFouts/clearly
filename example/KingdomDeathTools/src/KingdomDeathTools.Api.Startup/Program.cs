using DomainModeling.Crud.WebUi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddCrudWithUi(System.Reflection.Assembly.Load("KingdomDeathTools.Api.Services"));

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

app.UseStaticFiles();

app.MapControllers();

app.Run();