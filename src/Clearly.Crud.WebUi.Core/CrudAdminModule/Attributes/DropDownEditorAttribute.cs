// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.EntityGraph;

namespace Clearly.Crud.WebUi;

public class DropDownEditorAttribute : PropertyDefinitionNodeAttribute
{
    private readonly string options;

    /// <summary>
    /// Sets the Editor Component for this Property in the Admin to use a Drop Down Editor.
    /// </summary>
    /// <param name="options">A static set of options in the for of key value pairs. ex. "key1,value1;key2,value2"</param>
    public DropDownEditorAttribute(string options)
    {
        this.options = options;
    }

    /// <inheritdoc/>
    protected override void ApplyToDefinition(PropertyDefinitionNode property)
    {
        var adminFieldFeature = property.Using<CrudAdminPropertyFeature>();

        adminFieldFeature.EditorComponentName = SystemViewComponents.DropDownList;

        var dropDownFeature = property.Using<CrudAdminDropDownFeature>();
        dropDownFeature.DataSource = options;
        dropDownFeature.DataSourceType = "StaticList"; // TODO: Enum or constants for this value
    }
}
