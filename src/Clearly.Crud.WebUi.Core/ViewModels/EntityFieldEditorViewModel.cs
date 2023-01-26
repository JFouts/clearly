// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.Models.EntityGraph;

namespace Clearly.Crud.WebUi.ViewModels;

public record FieldEditorViewModel
{
    public PropertyDefinitionNode Definition { get; set; }
    public string FieldName { get; set; } = string.Empty;
    public string FieldEditorName { get; set; } = string.Empty;
    public object? Value { get; set; } // TODO: Options for boxing?
    public bool Hidden { get; set; }

    public FieldEditorViewModel(PropertyDefinitionNode definition)
    {
        Definition = definition;
    }
}
