using DomainModeling.Core.DomainObjectTypes;

namespace DomainModeling.Core.Handlers {
    public interface CommandHandler {
        void ExecuteCommand(object command);
    }

    public abstract class CommandHandler<T> : CommandHandler where T : Command {
      public void ExecuteCommand(object command) {
        if(command is T) {
          ExecuteCommand(command);
        }
      }

      public abstract void ExecuteCommand(T command);
    }
}
