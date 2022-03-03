// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.Search;

public class CrudSearchSortField
{
    public string Field { get; set; } = string.Empty;
    public CrudSearchSortDirection Direction { get; set; } = CrudSearchSortDirection.Ascending;
}
