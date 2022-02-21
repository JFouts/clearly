// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud.WebUi;

internal class CrudAdminEntityMetadata : IMetadata
{
    public string DataSourceUrl { get; set; } = string.Empty;
}

public class CrudAdminEntityFieldMetadata : IMetadata
{
    // TODO if we can us concrete types we can avoid boxing/unboxing
    public Dictionary<string, object> EditorProperties { get; set; } = new Dictionary<string, object>();
    public Dictionary<string, object> DisplayProperties { get; set; } = new Dictionary<string, object>();
    public string EditorViewComponentName { get; set; } = string.Empty;
    public string DisplayTemplate { get; set; } = string.Empty;
}
