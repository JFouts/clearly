// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.EntityGraph;
using Xunit;

namespace Clearly.Crud.Test.Unit;

public class EntityGraphFlattenedTests
{
    private readonly EntityDefinitionGraphFactory factory;
    private readonly EntityDefinitionGraphMapper mapper;

    public EntityGraphFlattenedTests()
    {
        factory = new EntityDefinitionGraphFactory(ModulePresets.DefaultModules);
        mapper = new EntityDefinitionGraphMapper();
    }

    [Fact]
    public void ItAddsRootNodeToFlattenedNodes()
    {
        // Arrange
        var graph = factory.CreateForEntity<BlankEntity>();

        // Act
        var flatNodes = mapper.Flatten(graph);

        // Assert
        Assert.True(flatNodes.ContainsKey("blankEntity"));
    }
    
    [Fact]
    public void FlattenNodeRetainsNodeKey()
    {
        // Arrange
        var graph = factory.CreateForEntity<BlankEntity>();

        // Act
        var flatNodes = mapper.Flatten(graph);

        // Assert
        Assert.Equal("blankEntity", flatNodes["blankEntity"].NodeKey);
    }

    [Fact]
    public void FlattenNodeRetainsDisplayName()
    {
        // Arrange
        var graph = factory.CreateForEntity<BlankEntity>();

        // Act
        var flatNodes = mapper.Flatten(graph);

        // Assert
        Assert.Equal("Blank Entity", flatNodes["blankEntity"].DisplayName);
    }

    [Fact]
    public void FlattenNodeRetainsProperties()
    {
        // Arrange
        var graph = factory.CreateForEntity<BlankEntity>();

        // Act
        var flatNodes = mapper.Flatten(graph);

        // Assert
        Enumerable.Any(flatNodes["blankEntity"].Properties, x => x.NodeKey == "id");
    }

    [Fact]
    public void FlattenNodeRetainsFeatures()
    {
        // Arrange
        var graph = factory.CreateForEntity<BlankEntity>();

        // Act
        var flatNodes = mapper.Flatten(graph);

        // Assert
        Assert.True(flatNodes["blankEntity"].Features.ContainsKey("crudadminentityfeature"));
    }

    [Fact]
    public void ItSerializesFeatures()
    {
        // Arrange
        var graph = factory.CreateForEntity<BlankEntity>();

        // Act
        var flatNodes = mapper.Flatten(graph);

        // Assert
        Assert.Equal("{\"DataSourceUrl\":\"/api/blankentity\"}", flatNodes["blankEntity"].Features["crudadminentityfeature"].ToString(Newtonsoft.Json.Formatting.None));
    }
}
