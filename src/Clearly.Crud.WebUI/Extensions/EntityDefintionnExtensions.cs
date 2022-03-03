// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.WebUi;

public static class EntityDefinitionnExtensions
{
    public static EntityDefinition CreateFor<TEntity>(this IEntityDefinitionFactory factory)
    {
        return factory.CreateForType(typeof(TEntity));
    }
}
