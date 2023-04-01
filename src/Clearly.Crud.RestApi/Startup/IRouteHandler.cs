// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Mvc;
using Clearly.Core;

namespace Clearly.Crud.RestApi;

public interface IRouteHandler<TDto, TEntity> where TEntity : class, IEntity, new()
{
    Task<IActionResult> Search(string query, int skip, int take);
    Task<IActionResult> Get(Guid id);
    Task<IActionResult> Post(TDto value);
    Task<IActionResult> Put(Guid id, TDto value);
    Task<IActionResult> Delete(Guid id);
}
