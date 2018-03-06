using System.Threading.Tasks;

namespace DomainModeling.Core.Interfaces
{
    public interface IAggregate<out TAggregate> where TAggregate : AggregateRoot
    {
        TAggregate State { get; }
        Task SaveAsync();
    }
}
