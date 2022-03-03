// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;

namespace Clearly.Crud.Services;

public abstract class DataSource<T> : IDataSource<T>
{
    public abstract Task<IEnumerable<T>> Load();

    async Task<IEnumerable> IDataSource.Load()
    {
        return await Load();
    }
}
