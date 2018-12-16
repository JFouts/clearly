using System;
using DomainModeling.Core.DomainObjectTypes;
using DomainModeling.Core.Handlers;

public class SampleScript : CommandHandler<SampleCommand> {
  private readonly Repository<SampleEntity> Repository;

  public SampleScript(Repository<SampleEntity> repository) {
    Repository = repository;
  }

  public override void ExecuteCommand(SampleCommand command) {
    var aggregate = Repository.Get(command.TargetId);
    aggregate.AggregateRoot.Value = "test";
    Repository.Save(aggregate);
  }
}

public class SampleEntity : AggregateRoot {
    public Guid Id { get; set; }
    public string Value { get; set; }
}

public class SampleCommand : Command {
    public Guid Id { get; set; }
    public Guid TargetId { get; set; }
}

var a = new SampleScript(null);
a
