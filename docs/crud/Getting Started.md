# Getting Started with Clearly CRUD

Clearly CRUD is designed to abstract away the boilerplate code that is written for most CRUD applications. With it you can design your domain models and your database and have a CRUD based REST API generated on top of those models along with an Admin interface for editing a viewing the models.

## Prerequisites

* Clearly CRUD is designed for .NET 6 and will not work on prior version of .NET
* You will need an ASP.NET Core project (https://docs.microsoft.com/en-us/visualstudio/get-started/csharp/tutorial-aspnet-core?view=vs-2022)
* If installing into an existing system you must have a unique GUID field on all entities

## Installation

There are two packages you need to install.

1. Clearly.Crud.RestAPI
2. Clearly.Crud.WebUI

These can be installed directly into your project through NuGet.

## Help Me Choose My Installation Options

### Admin and API All in One

One of the options for running Clearly CRUD is with the Admin interface and the API all in one service. This allows you to more easily package up your project for deployments and means you only need one  service running in the cloud or on your hosting environment.

This can be a great option for small teams or small projects that are looking for efficeny by having everything in one place.

### Admin and API Separated

Another option for running Clearly CRUD is to have two separated services that share a set of Domain Models. This allows you to deploy an admin and an API separately from one another 

This can be a great option if you want to scale up your API to meet the demands of your customers, but don't want the Admin to be scaled with the customer APIs.

### API Only

In this model there is no front end admin interface for your applications. Instead your entities are managed entirely though REST APIs.

This can be a great option if you are building your own Admin interface or you are integrating with an existing application or third party.
