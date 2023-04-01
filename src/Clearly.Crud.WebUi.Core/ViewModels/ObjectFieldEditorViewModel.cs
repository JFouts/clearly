// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.EntityGraph;

namespace Clearly.Crud.WebUi.ViewModels;

public record ObjectFieldEditorViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string FieldName { get; set; } = string.Empty;
    public ObjectTypeDefinitionNode Definition { get; set; }
    public IEnumerable<FieldEditorViewModel> Fields { get; set; } = new List<FieldEditorViewModel>();

    public ObjectFieldEditorViewModel(ObjectTypeDefinitionNode definition)
    {
        Definition = definition;
    }
}
