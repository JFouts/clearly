// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud;

public interface IEntityFieldModule : IModule
{
    void OnApplyingModule(EntityDefinition entity, EntityFieldDefinition field);
    void OnApplyingFallbackDefaults(EntityDefinition entity, EntityFieldDefinition field);
}
