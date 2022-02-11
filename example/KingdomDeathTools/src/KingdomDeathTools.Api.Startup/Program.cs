using DomainModeling.Crud.WebUi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddCrudWithUi(System.Reflection.Assembly.Load("KingdomDeathTools.Api.Services"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => {
    x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
        Version = "v1",
        Title = "Kingdom Death: Monster Tools"
    });
    x.TagActionsBy(x => 
        new List<string> { 
            x.RelativePath.StartsWith("admin/") ? "Admin Web UI" : x.ActionDescriptor.AttributeRouteInfo.Name?.Split('<').Last().Split('>').First(),
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => x.DocumentTitle = "API Docs - Kingdom Death: Monster Tools");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();