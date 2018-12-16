
using System;
using DomainModeling.Core.DomainObjectTypes;
using DomainModeling.Core.Exceptions;
using DomainModeling.Core.Utilities;
using DomainModeling.ObjectSourcing;
using Xunit;

public class BasicCrud {
    private readonly Factory<SampleEntity> Factory;
    private readonly Repository<SampleEntity> Repository;

    public BasicCrud() {
        Factory = new ObjectSourcedAggregateFactory<SampleEntity>();
        Repository = new InMemoryObjectSourcedAggregateRepository<SampleEntity>(new NewtonsoftJsonConverter());
    }

    [Fact]
    public void WhenAnAggregateIsCreatedItHasItsSpecifiedIdentity() {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var aggregate = Factory.Create(id);

        // Assert
        Assert.Equal(id, aggregate.Id);
    }

    [Fact]
    public void WhenAnAggregateIsRetrivedTheCorrectAggregateIsRetievedBasedOnItsIdentity() {
        // Arrange
        var id = Guid.NewGuid();
        Repository.Save(Factory.Create(id));

        // Act
        var aggregate = Repository.Get(id);

        // Assert
        Assert.Equal(id, aggregate.Id);
    }

    [Fact]
    public void WhenAnAggregateIsSavedTheVersionIsIncremented() {
        // Arrange
        var id = Guid.NewGuid();
        var old = Factory.Create(id);

        // Act
        Repository.Save(old);

        // Assert
        Assert.Equal(old.Version + 1, Repository.Get(id).Version);
    }

    [Fact]
    public void WhenAnAggregateIsSavedTheAggregateRootIsSavedWithIt() {
        // Arrange
        var id = Guid.NewGuid();
        var old = Factory.Create(id);
        old.AggregateRoot.Value = "sample";

        // Act
        Repository.Save(old);

        // Assert
        Assert.Equal("sample", Repository.Get(id).AggregateRoot.Value);
    }

    [Fact]
    public void WhenAnAggregateIsRetrivedThatDoesNotExistItThrowsAnError() {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        Action act = () => Repository.Get(id);

        // Assert
        var exception = Assert.Throws<NotFoundException>(act);
        Assert.Equal(id, exception.Id);
        Assert.False(string.IsNullOrWhiteSpace(exception.Message));
    }

    [Fact]
    public void WhenAnAggregateIsRetrievedItIsANewObject() {
        // Arrange
        var id = Guid.NewGuid();
        var old = Factory.Create(id);
        old.AggregateRoot.Value = "sample";

        // Act
        Repository.Save(old);
        var aggregate = Repository.Get(id);
        aggregate.AggregateRoot.Value = "newsample";

        // Assert
        Assert.Equal("sample", old.AggregateRoot.Value);
        Assert.Equal("newsample", aggregate.AggregateRoot.Value);
    }


    [Fact]
    public void WhenAnAggregateIsSavedButHasALowerVersionItThrowsAnError() {
        // Arrange
        var id = Guid.NewGuid();
        var old = Factory.Create(id);
        Repository.Save(old);

        // Act
        Action act = () => Repository.Save(old);

        // Assert
        var exception = Assert.Throws<VersionMismatchException>(act);
        Assert.Equal(id, exception.Id);
        Assert.Equal(old.Version, exception.ExpectedVersion);
        Assert.Equal(old.Version + 1, exception.ActualVersion);
        Assert.False(string.IsNullOrWhiteSpace(exception.Message));
    }

    private class SampleEntity : AggregateRoot {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
}
