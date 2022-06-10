// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.WebUi;

/// <summary>
/// Feature that controls how a field on a object type will be displayed in the CRUD Admin.
/// </summary>
public class CrudAdminFieldFeature : IFieldFeature
{
    /// <summary>
    /// Gets the properties for the <see cref="ViewCompoent"/> when rendering this field for editing.
    /// </summary>
    // TODO if we can use concrete types we can avoid boxing/unboxing
    public Dictionary<string, object> EditorProperties { get; } = new Dictionary<string, object>();

    /// <summary>
    /// Gets the properties for the display template when displaying this field for reading.
    /// </summary>
    // TODO if we can use concrete types we can avoid boxing/unboxing
    public Dictionary<string, object> DisplayProperties { get; } = new Dictionary<string, object>();

    /// <summary>
    /// Gets or sets the <see cref="ViewComponent"/> that should be used to render this field for editing.
    /// </summary>
    public string EditorViewComponentName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the display template the should be used when displaying this field for reading.
    /// </summary>
    public string DisplayTemplate { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether this field should be displayed in the admin search results table.
    /// </summary>
    public bool DisplayOnSearch { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the feild should be displayed in the editor.
    /// </summary>
    public bool DisplayInEditor { get; set; } = true;
}
