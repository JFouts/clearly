// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.Models.EntityGraph;

namespace Clearly.Crud;

public interface IDefinitionNodeModule
{
    void OnApplyingModule(DefinitionNode node);
    void OnApplyingFallbackDefaults(DefinitionNode node);
}
