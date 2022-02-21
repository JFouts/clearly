// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud.WebUi.ViewModels;

public record DropDownFieldEditorViewModel
{
    public string Id { get; set; } = string.Empty;
    public string FieldName { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public IEnumerable<DropDownOptionViewModel> Options { get; set; } = new List<DropDownOptionViewModel>();
}
