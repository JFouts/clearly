// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Dynamic;
using System.Text.Json;
using Clearly.Crud.Search;
using Clearly.Crud.Services;
using Moq;
using Xunit;

namespace Clearly.Crud.Test.Unit;

public class CrudEntityServiceTests
{
    private readonly CrudEntityService<BlankEntity> service;
    private readonly Mock<IEntityRepository<BlankEntity>> repository;

    public CrudEntityServiceTests()
    {
        repository = new Mock<IEntityRepository<BlankEntity>>();

        service = new CrudEntityService<BlankEntity>(repository.Object);
    }

    [Fact]
    public void ExpandoObjectFollowJsonConvertSettings()
    {
        dynamic example = new ExpandoObject();

        example.Property = "Test";

        var result = JsonSerializer.Serialize(example, options: new JsonSerializerOptions
        {
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
        });

        Assert.Equal("{\"property\":\"Test\"}", result);
    }

    [Fact]
    public async Task Insert_AddsEntityToRepository()
    {
        // Arrange
        var entity = new BlankEntity(Guid.Empty);

        // Act
        await service.Insert(entity);

        // Assert
        repository.Verify(x => x.Insert(entity), Times.Once);
    }

    [Fact]
    public async Task Insert_SetsTheEntityId()
    {
        // Arrange
        var entity = new BlankEntity(Guid.Empty);

        // Act
        await service.Insert(entity);

        // Assert
        Assert.NotEqual(Guid.Empty, entity.Id);
    }

    [Fact]
    public async Task GetById_RetrievesEntityFromRepositoryUsingId()
    {
        // Arrange
        var id = Guid.Parse("39a03186-d3eb-4c03-807b-d3d084c8a8d4");
        var expected = new BlankEntity(id);

        repository.Setup(x => x.GetById(id)).ReturnsAsync(expected);

        // Act
        var response = await service.GetById(id);

        // Assert
        Assert.Equal(expected, response);
    }

    [Fact]
    public async Task Search_RetrievesEntitiesFromRepositoryUsingSearchOptions()
    {
        // Arrange
        var searchOptions = new CrudSearchOptions();
        var expected = new CrudSearchResult<BlankEntity>();

        repository.Setup(x => x.Search(searchOptions)).ReturnsAsync(expected);

        // Act
        var response = await service.Search(searchOptions);

        // Assert
        Assert.Equal(expected, response);
    }

    [Fact]
    public async Task Update_SendsTheEntityToTheRepository()
    {
        // Arrange
        var id = Guid.Parse("2333c8c0-a1eb-4f25-8cc2-e60a1ee04bd5");
        var entity = new BlankEntity(id);

        // Act
        await service.Update(id, entity);

        // Assert
        repository.Verify(x => x.Update(id, entity), Times.Once);
    }

    [Fact]
    public async Task Update_AllowsIdToBeChanged()
    {
        // Arrange
        var existingId = Guid.Parse("d56fead4-90ab-4a1a-8eb4-ff157293448b");
        var newId = Guid.Parse("a21db427-eabb-472d-8bb2-c9497cb345d0");
        var entity = new BlankEntity(newId);

        // Act
        await service.Update(existingId, entity);

        // Assert
        repository.Verify(x => x.Update(existingId, entity), Times.Once);
    }

    [Fact]
    public async Task Delete_RemovesEntityToTheRepository()
    {
        // Arrange
        var id = Guid.Parse("ec15454e-89c1-41db-a6ac-0fede103f6fa");

        // Act
        await service.Delete(id);

        // Assert
        repository.Verify(x => x.Delete(id), Times.Once);
    }
}
