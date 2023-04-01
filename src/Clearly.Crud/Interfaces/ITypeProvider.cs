// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud;

public interface ITypeProvider
{
    IEnumerable<Type> GetTypes();
}
