// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud.WebUi.ViewModels;

public record TableDataViewModel : IPageableViewModel
{
    public int PageCount { get; set; }
    public int CurrentPage { get; set; }
    public IEnumerable<Dictionary<string, object>> Results { get; set; } = new Dictionary<string, object>[0];
    public IEnumerable<TableColumnViewModel> Columns { get; set; } = new List<TableColumnViewModel>();
}
