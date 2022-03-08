
# Creating Your First Domain Entity

## Prerequisites

Before creating your first entity you will need to add the required NuGet packages and configure your applications startup. If you haven't done this already check out the [Getting Started](GettingStarted.md) guide.

## Defining a New Entity

Defining a new Entity is as easy as creating a record _(or class)_ in your project the implements the `IEntity` interface.

```c#
using Clearly.Crud;

public record MyFirstEntity : IEntity
{
    public Guid Id { get; set; }
}
```
_NOTE: We recommend using records for Entities rather than classes and keeping them as POCOs._

By implementing the interface `IEntity` you are telling the system that this is a Domain Entity within this application and it will generate a REST API and Admin UI for you to edit this entity.

As part of the contract for the `IEntity` interface you must add an `Id` property of type `System.Guid`. This will be used in the API to lookup your entity. 

_NOTE: We recommend that this Id be the Primary Key for how your entity is stored in the database, however Clearly does not make this a requirement, it simply needs to be unique among all other entities of the same type._

With just this if we build an run our application we will get REST API endpoints generated at:
```
GET     /api/myfirstentity       /* For searching entities of this type */
GET     /api/myfirstentity/{id}  /* For retrieving an entity of this type by it's ID */
POST    /api/myfirstentity       /* For creating a new entity of this type */
PUT     /api/myfirstentity/{id}  /* For updating an entity of this type by it's ID */
DELETE  /api/myfirstentity/{id}  /* For deleting an entity of this type by it's ID */
```

## Adding Custom Fields

Now lets customize this entity a little bit to make it more useful. We will add a simple `Message` field as well as an `Author` field and then rename the record class to be `SocialMediaPost`:

```
using Clearly.Crud;

public record SocialMediaPost : IEntity
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public string Author { get; set; }
}
```

This is all you need to to for the system to add a new field to your endpoints and have this field be editable in the Admin interface.

## Testing it out through the Admin UI

### List of Type
If we visit `/admin/socialmediapost` we are presented with an empty table, this table lists out all the `SocialMediaPost` instances that have been created, which is empty for now. Lets click the Add New button to create a new one.

<!-- TODO: IMAGE ![This is an image](https://myoctocat.com/assets/images/base-octocat.svg) -->

### Create New

<!-- TODO: IMAGE ![This is an image](https://myoctocat.com/assets/images/base-octocat.svg) -->

From here we can fill in our message and author fields and click Create to save a new entry. We are returned to the table from before, but now our new post in there.

<!-- TODO: IMAGE ![This is an image](https://myoctocat.com/assets/images/base-octocat.svg) -->