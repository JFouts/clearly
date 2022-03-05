
# Creating Your First Domain Entity

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

Now lets add an additional field to this Entity:

```
using Clearly.Crud;

public record MyFirstEntity : IEntity
{
    public Guid Id { get; set; }
    public string Message { get; set; }
}
```

We will use this example field to store and retrieve a simple message. This is all you need to to for the system to add a new field to your endpoints and have this field be editable in the Admin interface.

