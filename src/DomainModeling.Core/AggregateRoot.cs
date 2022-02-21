// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Core;

public abstract record AggregateRoot : IEntity
{
    public Guid Id { get; set; }
}
