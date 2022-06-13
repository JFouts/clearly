// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.WebUi;

/// <summary>
/// Configuation for an Entity enabling it's use in the CRUD Admin.
/// </summary>
public class CrudAdminEntityFeature : IEntityFeature
{
    /// <summary>
    /// Gets or sets the base URL for the API endpoints controlling this Entity.
    /// </summary>
    public string DataSourceUrl { get; set; } = string.Empty;
}
