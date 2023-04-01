// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.EntityGraph;
using Xunit;

namespace Clearly.Crud.Test.Unit;

public class EntityGraphFactoryTests_NoModules
{
    private readonly EntityDefinitionGraphFactory factory;

    public EntityGraphFactoryTests_NoModules()
    {
        factory = new EntityDefinitionGraphFactory(ModulePresets.NoModules);
    }

    [Fact]
    public void ItDefaultsDisplayNameToDisplayFriendlyName_WhenNoModulesAreSet()
    {
        // Act
        var entity = factory.CreateForEntity<FullySpecifiedEntity>();

        // Assert
        Assert.Equal("Fully Specified Entity", entity.DisplayName);
    }

    [Fact]
    public void ItDefaultsNameKeyToEntityName_WhenNoModulesAreSet()
    {
        // Act
        var entity = factory.CreateForEntity<FullySpecifiedEntity>();

        // Assert
        Assert.Equal("fullySpecifiedEntity", entity.NodeKey);
    }
    
    [Fact]
    public void ItAddsPropertiesForProperties_WhenNoModulesAreSet()
    {
        // Act
        var entity = factory.CreateForEntity<FullySpecifiedEntity>();

        // Assert
        Assert.Contains(entity.Properties, x => x.Property.Name == nameof(FullySpecifiedEntity.Id));
    }

    [Fact]
    public void ItDoesNotAddPropertiesForClassProperties()
    {
        // Act
        var entity = factory.CreateForEntity<FullySpecifiedEntity>();

        // Assert
        Assert.DoesNotContain(entity.Properties, x => x.Property.Name.Contains("Field"));
    }

    [Fact]
    public void ItDoesAddPropertiesForClassProperties()
    {
        // Act
        var entity = factory.CreateForEntity<FullySpecifiedEntity>();

        // Assert
        Assert.Contains(entity.Properties, x => x.Property.Name.Contains("Property"));
    }

    [Fact]
    public void ItDoesNotAddPropertiesForPrivateClassMembers()
    {
        // Act
        var entity = factory.CreateForEntity<FullySpecifiedEntity>();

        // Assert
        Assert.DoesNotContain(entity.Properties, x => x.Property.Name.Contains("Private"));
    }

    [Fact]
    public void ItDoesNotAddPropertiesForInternalClassMembers()
    {
        // Act
        var entity = factory.CreateForEntity<FullySpecifiedEntity>();

        // Assert
        Assert.DoesNotContain(entity.Properties, x => x.Property.Name.Contains("Internal"));
    }

    [Fact]
    public void ItDoesNotAddPropertiesForProtectedClassMembers()
    {
        // Act
        var entity = factory.CreateForEntity<FullySpecifiedEntity>();

        // Assert
        Assert.DoesNotContain(entity.Properties, x => x.Property.Name.Contains("Protected"));
    }

    [Fact]
    public void ItDoesAddPropertiesForPublicClassMembers()
    {
        // Act
        var entity = factory.CreateForEntity<FullySpecifiedEntity>();

        // Assert
        Assert.Contains(entity.Properties, x => x.Property.Name.Contains("Public"));
    }

    [Fact]
    public void ItDoesAddPropertiesForBaseClassMembers()
    {
        // Act
        var entity = factory.CreateForEntity<FullySpecifiedEntity>();

        // Assert
        Assert.Contains(entity.Properties, x => x.Property.Name.Contains("Base"));
    }

    [Fact]
    public void ItDoesAddPropertiesForHiddenClassMembers()
    {
        // Act
        var entity = factory.CreateForEntity<FullySpecifiedEntity>();

        // Assert
        Assert.Contains(entity.Properties, x => x.Property.Name.Contains("Hidden"));
    }
    
    [Fact]
    public void ItDoesAddPropertiesForOverriddenClassMembers()
    {
        // Act
        var entity = factory.CreateForEntity<FullySpecifiedEntity>();

        // Assert
        Assert.Contains(entity.Properties, x => x.Property.Name.Contains("Overridden"));
    }

    [Fact]
    public void ItDoesAddPropertiesForVirtualClassMembers()
    {
        // Act
        var entity = factory.CreateForEntity<FullySpecifiedEntity>();

        // Assert
        Assert.Contains(entity.Properties, x => x.Property.Name.Contains("Virtual"));
    }

    [Fact]
    public void ItDoesAddPropertiesForComputedClassMembers()
    {
        // Act
        var entity = factory.CreateForEntity<FullySpecifiedEntity>();

        // Contains
        Assert.Contains(entity.Properties, x => x.Property.Name.Contains("Computed"));
    }

    [Fact]
    public void ItDoesAddPropertiesForReadonlyClassMembers()
    {
        // Act
        var entity = factory.CreateForEntity<FullySpecifiedEntity>();
 
        // Assert
        Assert.Contains(entity.Properties, x => x.Property.Name.Contains("Readonly"));
    }
    
    [Fact]
    public void ItOnlyAddsEachFieldOnce()
    {
        // Act
        var entity = factory.CreateForEntity<FullySpecifiedEntity>();

        // Assert
        Assert.Empty(entity.Properties.GroupBy(x => x.Property.Name).Where(x => x.Count() > 1));
    }
}
