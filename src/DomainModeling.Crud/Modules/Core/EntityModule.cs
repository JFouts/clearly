// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud;

public abstract class EntityModule : IEntityModule
{
    public virtual void OnApplyingModule(EntityDefinition entity)
    {
    }

    public virtual void OnApplyingFallbackDefaults(EntityDefinition entity)
    {
    }
}
