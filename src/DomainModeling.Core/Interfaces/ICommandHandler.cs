using System.Threading.Tasks;

namespace DomainModeling.Core.Interfaces
{
    public interface ICommandHandler<in T> where T : Command
    {
        Task ExecuteAsync(T command);
    }
}
