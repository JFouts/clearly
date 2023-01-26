// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;
using Clearly.Crud.Models.EntityGraph;
using Clearly.Crud.WebUi;
using Xunit;

namespace Clearly.Crud.Test.Unit;

public class EntityDefinitionFactoryTests_DefaultModules
{
    private readonly EntityDefinitionGraphFactory factory;

    public EntityDefinitionFactoryTests_DefaultModules()
    {
        var modules = new IDefinitionNodeModule[]
        {
            new AttributeBasedEntityModule(),
            new CoreEntityModule(),
            new CrudAdminModule(),
            new AttributeBasedEntityFieldModule(),
            new CoreEntityFieldModule(),
            new CrudAdminModule(),
        };

        factory = new EntityDefinitionGraphFactory(modules);
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
            entity.Properties
                .Single(x => x.Property.Name == nameof(DecoratedEntity.FieldWithCustomizedEditor))
                .Using<CrudAdminPropertyFeature>().EditorComponentName);
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
    //         entity.Properties
    //             .Single(x => x.Property.Name == nameof(DecoratedEntity.FieldWithCustomizedEditor))
    //             .Using<CrudAdminPropertyFeature>().EditorProperties["MyCustomProperty"]);
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
            entity.Properties
                .Single(x => x.Property.Name == nameof(BasicEntity.StringProperty))
                .Using<CrudAdminPropertyFeature>().DisplayComponentName);
    }

    [Fact]
    public void ItSetsDisplayTemplateForInts()
    {
        // Act
        var entity = factory.CreateForEntity<BasicEntity>();

        // Assert
        Assert.Equal(
            "NumberDisplayComponent", 
            entity.Properties
                .Single(x => x.Property.Name == nameof(BasicEntity.IntProperty))
                .Using<CrudAdminPropertyFeature>().DisplayComponentName);
    }
    
    [Fact]
    public void ItSetsDisplayTemplateForGuids()
    {
        // Act
        var entity = factory.CreateForEntity<BasicEntity>();

        // Assert
        Assert.Equal(
            "TextDisplayComponent", 
            entity.Properties
                .Single(x => x.Property.Name == nameof(BasicEntity.GuidProperty))
                .Using<CrudAdminPropertyFeature>().DisplayComponentName);
    }
       
    [Fact]
    public void ItSetsDisplayTemplateForEnumerable()
    {
        // Act
        var entity = factory.CreateForEntity<BasicEntity>();

        // Assert
        Assert.Equal(
            "TextArrayDisplayComponent", 
            entity.Properties
                .Single(x => x.Property.Name == nameof(BasicEntity.StringListProperty))
                .Using<CrudAdminPropertyFeature>().DisplayComponentName);
    }
       
    [Fact]
    public void ItSetsIdToHideOnSearch()
    {
        // Act
        var entity = factory.CreateForEntity<BasicEntity>();

        // Assert
        Assert.False(
            entity.Properties
                .Single(x => x.Property.Name == nameof(BasicEntity.Id))
                .Using<CrudAdminPropertyFeature>().DisplayOnSearch);
    }

    [Fact]
    public void ItSetsIdToHideInEditor()
    {
        // Act
        var entity = factory.CreateForEntity<BasicEntity>();

        // Assert
        Assert.False(
            entity.Properties
                .Single(x => x.Property.Name == nameof(BasicEntity.Id))
                .Using<CrudAdminPropertyFeature>().DisplayInEditor);
    }
}
