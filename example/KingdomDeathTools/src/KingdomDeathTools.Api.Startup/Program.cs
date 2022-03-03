using Clearly.Crud.RestApi;
using Clearly.Crud.WebUi;
using Clearly.Crud;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var domain = System.Reflection.Assembly.Load("KingdomDeathTools.Api.Services");
builder.Services.AddInMemoryEntityRepository(); // TODO: This is just for testing
builder.Services.AddCrudRestApi(domain);
builder.Services.AddCrudWebUi(domain);
// builder.Services.AddModule<AdminEditorDefinition>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => {
    x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
        Version = "v1",
        Title = "Kingdom Death: Monster Tools"
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