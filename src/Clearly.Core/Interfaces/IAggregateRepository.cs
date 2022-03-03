// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Core.Interfaces;

public interface IAggregateRepository<TAggregate>
    where TAggregate : AggregateRoot
{
    IAggregate<TAggregate> Instantiate(Guid id);
    Task<IAggregate<TAggregate>> RetrieveAsync(Guid id);
}
