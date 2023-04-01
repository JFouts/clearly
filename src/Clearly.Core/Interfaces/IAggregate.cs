// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Core.Interfaces;

public interface IAggregate<out TAggregate>
    where TAggregate : AggregateRoot
{
    TAggregate State { get; }
    Task SaveAsync();
}
