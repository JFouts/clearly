// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.Services;

public class StaticListDataSource : DataSource<KeyValuePair<string, string>>
{
    private readonly IEnumerable<KeyValuePair<string, string>> data;

    public StaticListDataSource(IEnumerable<KeyValuePair<string, string>> data)
    {
        this.data = data;
    }

    public override Task<IEnumerable<KeyValuePair<string, string>>> Load()
    {
        return Task.FromResult(data);
    }
}
