// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.Models.EntityGraph;

namespace Clearly.Crud.WebUi.ViewModels;

public record EntityEditorViewModel
{
    public EntityTypeDefinitionNode Definition { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string DataSourceUrl { get; set; } = string.Empty;
    public IEnumerable<FieldEditorViewModel> Fields { get; set; } = new List<FieldEditorViewModel>();

    public EntityEditorViewModel(EntityTypeDefinitionNode definition)
    {
        Definition = definition;
    }
}
