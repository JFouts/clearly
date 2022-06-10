// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.WebUi.ViewModels;

public record ObjectFieldEditorViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string FieldName { get; set; } = string.Empty;
    public ObjectTypeDefinition Definition { get; set; }
    public IEnumerable<FieldEditorViewModel> Fields { get; set; } = new List<FieldEditorViewModel>();

    public ObjectFieldEditorViewModel(EntityDefinition definition)
    {
        Definition = definition;
    }
}
