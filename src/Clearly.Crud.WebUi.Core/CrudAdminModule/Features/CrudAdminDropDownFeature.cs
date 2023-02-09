// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.EntityGraph;

namespace Clearly.Crud.WebUi;

public class CrudAdminDropDownFeature : IDefinitionFeature
{
    public string DataSource { get; set; } = string.Empty;
    public string DataSourceType { get; set; } = string.Empty;
}