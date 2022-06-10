// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.WebUi.ViewModels;

public record EntityEditorViewModel
{
    public EntityDefinition Definition { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string DataSourceUrl { get; set; } = string.Empty;
    public IEnumerable<FieldEditorViewModel> Fields { get; set; } = new List<FieldEditorViewModel>();

    public EntityEditorViewModel(EntityDefinition definition)
    {
        Definition = definition;
    }
}
