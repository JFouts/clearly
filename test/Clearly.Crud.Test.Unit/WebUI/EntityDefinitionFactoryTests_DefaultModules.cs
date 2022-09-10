// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;
using Clearly.Crud.WebUi;
using Xunit;

namespace Clearly.Crud.Test.Unit;

public class EntityDefinitionFactoryTests_DefaultModules
{
    private readonly EntityDefinitionFactory factory;

    public EntityDefinitionFactoryTests_DefaultModules()
    {
        var entityModules = new IEntityModule[]
        {
            new AttributeBasedEntityModule(),
            new CoreEntityModule(),
            new CrudAdminModule(),
        };

        var fieldModules = new IEntityFieldModule[]
        {
            new AttributeBasedEntityFieldModule(),
            new CoreEntityFieldModule(),
            new CrudAdminModule(),
        };

        factory = new EntityDefinitionFactory(entityModules, fieldModules);
    }

    [Fact]
    public void ItSetsDataSourceUrl()
    {
        // Act
        var entity = factory.CreateForEntity<BlankEntity>();

        // Assert
        Assert.Equal("/api/blankentity", entity.Using<CrudAdminEntityFeature>().DataSourceUrl);
    }

    public record DecoratedEntity : IEntity
    {
        public Guid Id { get; set; }

        [FieldEditor("MyCustomViewComponent")]
        // TODO: Some way of defining this:
        // [FieldEditorProperty("MyCustomProperty", "A Custom Value")]
        public string FieldWithCustomizedEditor { get; set; } = string.Empty;
    }

    [Fact]
    public void ItSetsEditorViewComponentWhenDecorated()
    {
        // Act
        var entity = factory.CreateForEntity<DecoratedEntity>();

        // Assert
        Assert.Equal(
            "MyCustomViewComponent", 
            entity.Fields
                .Single(x => x.Property.Name == nameof(DecoratedEntity.FieldWithCustomizedEditor))
                .Using<CrudAdminFieldFeature>().EditorComponentName);
    }

    // TODO: Fix this test when we have a way to set custom properties on editors
    // [Fact]
    // public void ItSetsEditorPropertiesWhenDecorated()
    // {
    //     // Act
    //     var entity = factory.CreateForEntity<DecoratedEntity>();
    // 
    //     // Assert
    //     Assert.Equal(
    //         "A Custom Value", 
    //         entity.Fields
    //             .Single(x => x.Property.Name == nameof(DecoratedEntity.FieldWithCustomizedEditor))
    //             .Using<CrudAdminFieldFeature>().EditorProperties["MyCustomProperty"]);
    // }
    
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
        var entity = factory.CreateForEntity<BasicEntity>();

        // Assert
        Assert.Equal(
            "TextDisplayComponent", 
            entity.Fields
                .Single(x => x.Property.Name == nameof(BasicEntity.StringProperty))
                .Using<CrudAdminFieldFeature>().DisplayComponentName);
    }

    [Fact]
    public void ItSetsDisplayTemplateForInts()
    {
        // Act
        var entity = factory.CreateForEntity<BasicEntity>();

        // Assert
        Assert.Equal(
            "NumberDisplayComponent", 
            entity.Fields
                .Single(x => x.Property.Name == nameof(BasicEntity.IntProperty))
                .Using<CrudAdminFieldFeature>().DisplayComponentName);
    }
    
    [Fact]
    public void ItSetsDisplayTemplateForGuids()
    {
        // Act
        var entity = factory.CreateForEntity<BasicEntity>();

        // Assert
        Assert.Equal(
            "TextDisplayComponent", 
            entity.Fields
                .Single(x => x.Property.Name == nameof(BasicEntity.GuidProperty))
                .Using<CrudAdminFieldFeature>().DisplayComponentName);
    }
       
    [Fact]
    public void ItSetsDisplayTemplateForEnumerable()
    {
        // Act
        var entity = factory.CreateForEntity<BasicEntity>();

        // Assert
        Assert.Equal(
            "TextArrayDisplayComponent", 
            entity.Fields
                .Single(x => x.Property.Name == nameof(BasicEntity.StringListProperty))
                .Using<CrudAdminFieldFeature>().DisplayComponentName);
    }
       
    [Fact]
    public void ItSetsIdToHideOnSearch()
    {
        // Act
        var entity = factory.CreateForEntity<BasicEntity>();

        // Assert
        Assert.False(
            entity.Fields
                .Single(x => x.Property.Name == nameof(BasicEntity.Id))
                .Using<CrudAdminFieldFeature>().DisplayOnSearch);
    }

    [Fact]
    public void ItSetsIdToHideInEditor()
    {
        // Act
        var entity = factory.CreateForEntity<BasicEntity>();

        // Assert
        Assert.False(
            entity.Fields
                .Single(x => x.Property.Name == nameof(BasicEntity.Id))
                .Using<CrudAdminFieldFeature>().DisplayInEditor);
    }
}
