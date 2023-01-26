// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.Models.EntityGraph;

public interface IEntityDefinitionGraphFactory
{
    ObjectTypeDefinitionNode CreateForType(Type type);
    Dictionary<string, TypeDefinitionNodeFlattened> CreateFlattenedForType(Type type);
    Dictionary<string, TypeDefinitionNodeFlattened> Flatten(TypeDefinitionNode rootNode);
}
