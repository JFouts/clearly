# Domain Modeling
Domain Modeling is a library that is intended to facilitate Domain Driven Design (DDD) in .Net Core applications. The goal is to abstract the technical details associated with your application so that the application code focuses deeply on the business domain.

## Aggregate
>  A DDD aggregate is a cluster of domain objects that can be treated as a single unit. An example may be an order and its line-items, these will be separate objects, but it's useful to treat the order (together with its line items) as a single aggregate.
> *-Martain Fowler*

All aggregates in your application should inherit from `AggregateRoot`. 
*Note: It is enforced that all aggregates be identified by a GUID.*

Example (*The Question aggregate from the Questionable application.*):

    public class Question : AggregateRoot
    {
	  public Guid UserId { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      public int Likes { get; set; }
      public bool HasAcceptedAnswer { get; set; }
      public Answer AcceptedAnswer { get; set; }
      public IList<Answer> Answers { get; set; } = new List<Answer>();
      public IList<Guid> LikedUsers { get; set; } = new List<Guid>();
      public IList<string> Tags { get; set; } = new List<string>();

      public class Answer
      {
        public Guid AnswerId { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }
      }
    }


### Loading Aggregates
Aggregates should always be loaded in their entirety prior to modifying it's state. This allows you to fully validate that no business rules are broken before and after the change is applied to the aggregate's state as well as allowing for to assert that the state of the aggregate has not changed since those validation have occurred. If it is a performance constrain to load the aggregate it is recommended that you reassess how your aggregate is stored or where the true aggregate root lies.
To load an aggregate into your application use an `IAggregateRepository<TAggregate>`. This will allow you to load either a new aggregate or load the state of an existing aggregate. 

### Modifying Aggregates

Depends on if you are doing CRUD or Event Sourcing

### Saving Aggregates

All aggregates loaded using `IAggregateRepository<TAggregate>` are wrapped in an `IAggregate<TAggregate>` to allow for the state changes on the aggregate to be saved back once processing is completed.
Example:

    public async Task ExecuteAsync(AnswerQuestionCommand command)
    {
      var question = await _aggregateRepository.RetrieveAsync(command.QuestionId);
      // Processing Omitted
      await question.SaveAsync();
    }

## Domain Event
### Creating a Domain Event
### Recording a Domain Event
### Processing a Domain Event
## Command
## Event Sourcing
### Event Store
## CRUD
### Mongo DB
### Cosmos DB
## Asp Net Core