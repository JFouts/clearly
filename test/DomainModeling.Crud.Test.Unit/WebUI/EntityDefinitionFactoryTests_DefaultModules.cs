// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using DomainModeling.Core;
using DomainModeling.Crud.WebUi;
using Xunit;

namespace DomainModeling.Crud.Test.Unit;

public class EntityDefinitionFactoryTests_DefaultModules
{
    private readonly EntityDefinitionFactory factory;

    public EntityDefinitionFactoryTests_DefaultModules()
    {
        var entityModules = new IEntityModule[]
        {
            new AttributeBasedEntityModule(),
            new CoreEntityModule(),
            new CrudAdminEntityModule(),
         };

        var fieldModules = new IEntityFieldModule[]
        {
            new AttributeBasedEntityFieldModule(),
            new CoreEntityFieldModule(),
            new CrudAdminEntityFieldModule(),
        };

        factory = new EntityDefinitionFactory(entityModules, fieldModules);
    }

    [Fact]
    public void ItSetsDataSourceUrl()
    {
        // Act
        var entity = factory.CreateFor<BlankEntity>();

        // Assert
        Assert.Equal("/api/blankentity", entity.UsingMetadata<CrudAdminEntityMetadata>().DataSourceUrl);
    }

    public record DecoratedEntity : IEntity
    {
        public Guid Id { get; set; }

        [FieldEditor("MyCustomViewComponent")]
        [FieldEditorProperty("MyCustomProperty", "A Custom Value")]
        public string FieldWithCustomizedEditor { get; set; } = string.Empty;
    }

    [Fact]
    public void ItSetsEditorViewComponentWhenDecorated()
    {
        // Act
        var entity = factory.CreateFor<DecoratedEntity>();

        // Assert
        Assert.Equal(
            "MyCustomViewComponent", 
            entity.Fields
                .Single(x => x.Property.Name == nameof(DecoratedEntity.FieldWithCustomizedEditor))
                .UsingMetadata<CrudAdminEntityFieldMetadata>().EditorViewComponentName);
    }

    [Fact]
    public void ItSetsEditorPropertiesWhenDecorated()
    {
        // Act
        var entity = factory.CreateFor<DecoratedEntity>();

        // Assert
        Assert.Equal(
            "A Custom Value", 
            entity.Fields
                .Single(x => x.Property.Name == nameof(DecoratedEntity.FieldWithCustomizedEditor))
                .UsingMetadata<CrudAdminEntityFieldMetadata>().EditorProperties["MyCustomProperty"]);
    }
    
    public record BasicEntity : IEntity
    {
        public Guid Id { get; set; }

        public string StringProperty { get; set; } = string.Empty;
        public int IntProperty { get; set; }
        public Guid GuidProperty { get; set; }
        public IEnumerable<string> StringListProperty { get; set; } = new List<string>();
    }

    [Fact]
    public void ItSetsDisplayTemplateForStrings()
    {
        // Act
        var entity = factory.CreateFor<BasicEntity>();

        // Assert
        Assert.Equal(
            "String", 
            entity.Fields
                .Single(x => x.Property.Name == nameof(BasicEntity.StringProperty))
                .UsingMetadata<CrudAdminEntityFieldMetadata>().DisplayTemplate);
    }

    [Fact]
    public void ItSetsDisplayTemplateForInts()
    {
        // Act
        var entity = factory.CreateFor<BasicEntity>();

        // Assert
        Assert.Equal(
            "Int32", 
            entity.Fields
                .Single(x => x.Property.Name == nameof(BasicEntity.IntProperty))
                .UsingMetadata<CrudAdminEntityFieldMetadata>().DisplayTemplate);
    }
    
    [Fact]
    public void ItSetsDisplayTemplateForGuids()
    {
        // Act
        var entity = factory.CreateFor<BasicEntity>();

        // Assert
        Assert.Equal(
            "Guid", 
            entity.Fields
                .Single(x => x.Property.Name == nameof(BasicEntity.GuidProperty))
                .UsingMetadata<CrudAdminEntityFieldMetadata>().DisplayTemplate);
    }
       
    [Fact]
    public void ItSetsDisplayTemplateForEnumerable()
    {
        // Act
        var entity = factory.CreateFor<BasicEntity>();

        // Assert
        Assert.Equal(
            "IEnumerable`1", 
            entity.Fields
                .Single(x => x.Property.Name == nameof(BasicEntity.StringListProperty))
                .UsingMetadata<CrudAdminEntityFieldMetadata>().DisplayTemplate);
    }
       
    [Fact]
    public void ItSetsIdToHideOnSearch()
    {
        // Act
        var entity = factory.CreateFor<BasicEntity>();

        // Assert
        Assert.False(
            entity.Fields
                .Single(x => x.Property.Name == nameof(BasicEntity.Id))
                .UsingMetadata<CrudAdminEntityFieldMetadata>().DisplayOnSearch);
    }

    [Fact(Skip = "Not Yet Implemented")]
    public void ItSetsIdToHideInEditor()
    {
        // Act
        var entity = factory.CreateFor<BasicEntity>();

        // Assert
        Assert.False(
            entity.Fields
                .Single(x => x.Property.Name == nameof(BasicEntity.Id))
                .UsingMetadata<CrudAdminEntityFieldMetadata>().DisplayInEditor);
    }
}
