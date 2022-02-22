// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using DomainModeling.Core;

namespace DomainModeling.Crud.Test.Unit;

public record BlankEntity : IEntity
{
    public Guid Id { get; set; }

    public BlankEntity(Guid id)
    {
        Id = id;
    }
}
