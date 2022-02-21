// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud.Services;

public interface IDataSource<T> : IDataSource
{
    new Task<IEnumerable<T>> Load();
}
