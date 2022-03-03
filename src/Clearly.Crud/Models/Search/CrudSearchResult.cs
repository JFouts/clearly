// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.Search;

public class CrudSearchResult<T>
{
    public int Count { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
    public IAsyncEnumerable<T> Results { get; set; } = AsyncEnumerable.Empty<T>();
}
