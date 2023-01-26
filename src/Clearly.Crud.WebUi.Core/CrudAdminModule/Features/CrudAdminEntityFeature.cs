// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.Models.EntityGraph;

namespace Clearly.Crud.WebUi;

/// <summary>
/// Configuration for an Entity enabling it's use in the CRUD Admin.
/// </summary>
public class CrudAdminEntityFeature : IDefinitionFeature
{
    /// <summary>
    /// Gets or sets the base URL for the API endpoints controlling this Entity.
    /// </summary>
    public string DataSourceUrl { get; set; } = string.Empty;
}
