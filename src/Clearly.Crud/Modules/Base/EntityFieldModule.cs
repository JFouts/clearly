// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud;

public abstract class EntityFieldModule : IEntityFieldModule
{
    public virtual void OnApplyingModule(EntityDefinition entity, FieldDefinition field)
    {
    }

    public virtual void OnApplyingFallbackDefaults(EntityDefinition entity, FieldDefinition field)
    {
    }
}
