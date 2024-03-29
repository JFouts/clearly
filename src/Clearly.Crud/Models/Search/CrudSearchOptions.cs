// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.Search;

public class CrudSearchOptions
{
    public int Skip { get; set; }
    public int Take { get; set; }
    public string SearchQuery { get; set; } = string.Empty;
    public CrudSearchFilter Filters { get; set; } = new CrudSearchFilter();
    public IEnumerable<CrudSearchSortField> SortFields { get; set; } = new List<CrudSearchSortField>();
}
