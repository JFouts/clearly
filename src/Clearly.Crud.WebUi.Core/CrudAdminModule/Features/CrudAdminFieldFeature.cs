// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.WebUi;

/// <summary>
/// Feature that controls how a field on a object type will be displayed in the CRUD Admin.
/// </summary>
public class CrudAdminFieldFeature : IFieldFeature
{
    /// <summary>
    /// Gets or sets the Blazor Component that should be used to render this field for editing.
    /// </summary>
    public string EditorComponentName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Blazor Component the should be used when displaying this field for reading.
    /// </summary>
    public string DisplayComponentName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether this field should be displayed in the admin search results table.
    /// </summary>
    public bool DisplayOnSearch { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the feild should be displayed in the editor.
    /// </summary>
    public bool DisplayInEditor { get; set; } = true;
}
