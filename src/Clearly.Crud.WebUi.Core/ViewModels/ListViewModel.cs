// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.WebUi.ViewModels;

public record ListViewModel : TableDataViewModel
{
    public string DisplayName { get; set; } = string.Empty;
}
