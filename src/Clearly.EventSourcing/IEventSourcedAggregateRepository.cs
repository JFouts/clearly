using System;
using System.Threading.Tasks;
using Clearly.Core;
using Clearly.Core.Interfaces;

namespace Clearly.EventSourcing
{
    public interface IEventSourcedAggregateRepository<TAggregate>  : IAggregateRepository<TAggregate> where TAggregate : AggregateRoot, new()
    {
        new IEventSourcedAggregate<TAggregate> Instantiate(Guid id);
        new Task<IEventSourcedAggregate<TAggregate>> RetrieveAsync(Guid id);
        Task SaveUnrecordedAggregateAsync(UnrecordedAggregate<TAggregate> unrecordedAggregate);
        Task SaveRecordedAggregateAsync(RecordedAggregate<TAggregate> recordedAggregate);
    }
}
