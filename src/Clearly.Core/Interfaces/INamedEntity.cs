// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Core;

public interface INamedEntity : IEntity
{
    string Name { get; set; }
}
