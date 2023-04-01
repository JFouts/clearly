// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;
namespace Clearly.Crud.RestApi;

public class EntityReference<TEntity> : EntityReference where TEntity : IEntity
{
    public TEntity? Entity { get; set; }
}

public class EntityReference
{
    public Guid Id { get; set; }
}