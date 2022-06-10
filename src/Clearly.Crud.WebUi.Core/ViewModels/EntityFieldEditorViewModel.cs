// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.WebUi.ViewModels;

public record FieldEditorViewModel
{
    public FieldDefinition Definition { get; set; }
    public string FieldName { get; set; } = string.Empty;
    public string FieldEditorName { get; set; } = string.Empty;
    public object? Value { get; set; } // TODO: Options for boxing?
    public bool Hidden { get; set; }

    public FieldEditorViewModel(FieldDefinition definition)
    {
        Definition = definition;
    }
}
