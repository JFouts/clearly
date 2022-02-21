// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;

namespace DomainModeling.Crud.Services;

public interface IDataSource
{
    Task<IEnumerable> Load();
}
