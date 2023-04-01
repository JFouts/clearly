# Getting Started with Clearly CRUD

Clearly CRUD is designed to abstract away the boilerplate code that is written for most CRUD applications. With it you can design your domain models and your database and have a CRUD based REST API generated on top of those models along with an Admin interface for editing a viewing the models.

## Prerequisites

* Clearly CRUD is designed for .NET 6 and will not work on prior versions of .NET
* You will need an ASP.NET Core project (https://docs.microsoft.com/en-us/visualstudio/get-started/csharp/tutorial-aspnet-core?view=vs-2022)
* If installing into an existing system you must have a unique GUID field on all entities

## Installation

There are two packages you need to install.

1. Clearly.Crud.RestAPI (TODO: Link to nuget)
2. Clearly.Crud.WebUI (TODO: Link to nuget)

These can be installed directly into your project through NuGet.

You can install these packages into the same project for a shared Admin/API or into separate Web Applications for a Admin and API split. Checkout out [Help Me Choose My Installation Options](#help-me-choose-my-installation-options) for more details on making this decision.

## Configuring Your Startup

Here we will show the startup configuration using the latest .NET 6 startup format, however this could easily be adapted to the more verbose `Startup.cs` style.

### Configuring CRUD Rest API
_Program.cs_
```c# 
using Clearly.Crud.RestApi;

var builder = WebApplication.CreateBuilder(args);

var domain = System.Reflection.Assembly.Load("MyProject.DomainProject");

builder.Services.AddCrudRestApi(domain);

/* Register any other services you might need */

var app = builder.Build();

app.UseCrudErrorHandler();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
```

The first step is to get a reference to the assembly that will have your Domain Models in it. This may be a services project or a shared models project, or it could be multiple projects in which case you would just need a reference to each project's assembly.

Then we call `AddCrudRestApi` which will add all the required services for the CRUD Rest API to run as well as calling the ASP.MET extension `AddControllers` to enable Controller based routing on your application. 

Then we follow a standard application pipeline setup. Near the top of the pipeline we add `UseCrudErrorHandler` which will add a middleware to the pipeline that will catch common exception types an map them to their corresponding HTTP status code.

Then towards the end of the pipeline we call `MapControllers` this is the standard ASP.NET method for adding controller based routing to the pipeline and is required for Clearly CRUD to handle your API requests.

### Configuring CRUD Admin UI
_Program.cs_
```c# 
using Clearly.Crud.RestApi;

var builder = WebApplication.CreateBuilder(args);

var domain = System.Reflection.Assembly.Load("MyProject.DomainProject");

builder.Services.AddCrudWebUi(domain);

/* Register any other services you might need */

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.UseClearlyAdmin();

app.Run();
```

The first step is to get a reference to the assembly that will have your Domain Models in it. This may be a services project or a shared models project, or it could be multiple projects in which case you would just need a reference to each project's assembly.

Then we call `AddCrudWebUi` which will add all the required services for the CRUD Admin WebUI to run as well as calling the ASP.MET extension `AddMvc` to enable Controller's and Razor Pages based routing on your application.

Then we follow a standard application pipeline setup.

Then towards the end of the pipeline we call `UseClearlyAdmin` this will add the required blazor static files as well as map anything under the `/admin` route to the Clearly CRUD Admin UI blazor application. 


## Help Me Choose My Installation Options

### Admin and API All in One

One of the options for running Clearly CRUD is with the Admin interface and the API all in one service. This allows you to more easily package up your project for deployments and means you only need one  service running in the cloud or on your hosting environment.

This can be a great option for small teams or small projects that are looking for efficiency by having everything in one place.

### Admin and API Separated

Another option for running Clearly CRUD is to have two separated services that share a set of Domain Models. This allows you to deploy an admin and an API separately from one another 

This can be a great option if you want to scale up your API to meet the demands of your customers, but don't want the Admin to be scaled with the customer facing APIs.

### API Only

In this model there is no front end admin interface for your applications. Instead your entities are managed entirely though REST APIs.

This can be a great option if you are building your own Admin interface or if you are integrating with an existing application or third party.


## Continue Reading

* [Creating Your First Entity](MyFirstEntity.md)