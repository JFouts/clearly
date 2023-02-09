// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.Services;

public class FuncDataSource<T> : DataSource<T>
{
    private readonly Func<Task<IEnumerable<T>>> load;

    public FuncDataSource(Func<Task<IEnumerable<T>>> load)
    {
        this.load = load;
    }

    public override async Task<IEnumerable<T>> Load()
    {
        return await load();
    }
}
