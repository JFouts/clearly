// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Core;

/// <summary>
/// An Entity is an object that is uniquely identified by an id rather than the 
/// values of the object.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets or sets the identifying value of the object.
    /// </summary>
    Guid Id { get; set; }
}
