// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.EntityGraph;
namespace Clearly.Crud.RestApi;

public class CrudApiFeature : IDefinitionFeature
{
    public Type? DtoType { get; set; }
    public Type? RefType { get; set; }
}
