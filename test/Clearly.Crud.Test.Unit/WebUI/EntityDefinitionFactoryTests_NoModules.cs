// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.WebUi;
using Xunit;

namespace Clearly.Crud.Test.Unit;

public class EntityDefinitionFactoryTests_NoModules
{
    private readonly EntityDefinitionFactory factory;

    public EntityDefinitionFactoryTests_NoModules()
    {
        factory = new EntityDefinitionFactory(new IEntityModule[0], new IEntityFieldModule[0]);
    }

    [Fact]
    public void ItDefaultsDisplayNameToDisplayFriendlyName_WhenNoModulesAreSet()
    {
        // Act
        var entity = factory.CreateFor<FullySpecifiedEntity>();

        // Assert
        Assert.Equal("Fully Specified Entity", entity.DisplayName);
    }

    [Fact]
    public void ItDefaultsNameKeyToEntityName_WhenNoModulesAreSet()
    {
        // Act
        var entity = factory.CreateFor<FullySpecifiedEntity>();

        // Assert
        Assert.Equal(nameof(FullySpecifiedEntity), entity.NameKey);
    }
    
    [Fact]
    public void ItAddsFieldsForProperties_WhenNoModulesAreSet()
    {
        // Act
        var entity = factory.CreateFor<FullySpecifiedEntity>();

        // Assert
        Assert.Contains(entity.Fields, x => x.Property.Name == nameof(FullySpecifiedEntity.Id));
    }

    [Fact]
    public void ItDoesNotAddFieldsForClassFields()
    {
        // Act
        var entity = factory.CreateFor<FullySpecifiedEntity>();

        // Assert
        Assert.DoesNotContain(entity.Fields, x => x.Property.Name.Contains("Field"));
    }

    [Fact]
    public void ItDoesAddFieldsForClassProperties()
    {
        // Act
        var entity = factory.CreateFor<FullySpecifiedEntity>();

        // Assert
        Assert.Contains(entity.Fields, x => x.Property.Name.Contains("Property"));
    }

    [Fact]
    public void ItDoesNotAddFieldsForPrivateClassMembers()
    {
        // Act
        var entity = factory.CreateFor<FullySpecifiedEntity>();

        // Assert
        Assert.DoesNotContain(entity.Fields, x => x.Property.Name.Contains("Private"));
    }

    [Fact]
    public void ItDoesNotAddFieldsForInternalClassMembers()
    {
        // Act
        var entity = factory.CreateFor<FullySpecifiedEntity>();

        // Assert
        Assert.DoesNotContain(entity.Fields, x => x.Property.Name.Contains("Internal"));
    }

    [Fact]
    public void ItDoesNotAddFieldsForProtectedClassMembers()
    {
        // Act
        var entity = factory.CreateFor<FullySpecifiedEntity>();

        // Assert
        Assert.DoesNotContain(entity.Fields, x => x.Property.Name.Contains("Protected"));
    }

    [Fact]
    public void ItDoesAddFieldsForPublicClassMembers()
    {
        // Act
        var entity = factory.CreateFor<FullySpecifiedEntity>();

        // Assert
        Assert.Contains(entity.Fields, x => x.Property.Name.Contains("Public"));
    }

    [Fact]
    public void ItDoesAddFieldsForBaseClassMembers()
    {
        // Act
        var entity = factory.CreateFor<FullySpecifiedEntity>();

        // Assert
        Assert.Contains(entity.Fields, x => x.Property.Name.Contains("Base"));
    }

    [Fact]
    public void ItDoesAddFieldsForHiddenClassMembers()
    {
        // Act
        var entity = factory.CreateFor<FullySpecifiedEntity>();

        // Assert
        Assert.Contains(entity.Fields, x => x.Property.Name.Contains("Hidden"));
    }
    
    [Fact]
    public void ItDoesAddFieldsForOverriddenClassMembers()
    {
        // Act
        var entity = factory.CreateFor<FullySpecifiedEntity>();

        // Assert
        Assert.Contains(entity.Fields, x => x.Property.Name.Contains("Overridden"));
    }

    [Fact]
    public void ItDoesAddFieldsForVirtualClassMembers()
    {
        // Act
        var entity = factory.CreateFor<FullySpecifiedEntity>();

        // Assert
        Assert.Contains(entity.Fields, x => x.Property.Name.Contains("Virtual"));
    }

    [Fact]
    public void ItDoesAddFieldsForComputedClassMembers()
    {
        // Act
        var entity = factory.CreateFor<FullySpecifiedEntity>();

        // Contains
        Assert.Contains(entity.Fields, x => x.Property.Name.Contains("Computed"));
    }

    [Fact]
    public void ItDoesAddFieldsForReadonlyClassMembers()
    {
        // Act
        var entity = factory.CreateFor<FullySpecifiedEntity>();
 
        // Assert
        Assert.Contains(entity.Fields, x => x.Property.Name.Contains("Readonly"));
    }
    
    [Fact]
    public void ItOnlyAddsEachFieldOnce()
    {
        // Act
        var entity = factory.CreateFor<FullySpecifiedEntity>();

        // Assert
        Assert.Empty(entity.Fields.GroupBy(x => x.Property.Name).Where(x => x.Count() > 1));
    }
}
