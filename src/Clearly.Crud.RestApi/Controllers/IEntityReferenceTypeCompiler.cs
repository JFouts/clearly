// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.EntityGraph;
namespace Clearly.Crud.RestApi;

public interface IEntityReferenceTypeCompiler
{
    Type Compile(ObjectTypeDefinitionNode type);
}