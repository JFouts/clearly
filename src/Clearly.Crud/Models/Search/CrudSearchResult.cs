// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.Search;

/// <summary>
/// A paginated search results container.
/// </summary>
/// <typeparam name="T">Data type of the search result records.</typeparam>
public class CrudSearchResult<T>
{
    /// <summary>
    /// Gets or sets the total number of records in the search results.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Gets or sets the number of records skipped for pagination.
    /// </summary>
    public int Skip { get; set; }

    /// <summary>
    /// Gets or sets the number of records retrieved for pagination.
    /// </summary>
    public int Take { get; set; }
    
    /// <summary>
    /// Gets or sets the search result records.
    /// </summary>
    public IEnumerable<T> Results { get; set; } = Array.Empty<T>();
}
