// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.EntityGraph;
namespace Clearly.Crud.RestApi;

public class CrudApiModule : DefinitionNodeModule<EntityTypeDefinitionNode>
{
    private readonly IEntityDtoCompiler entityDtoCompiler;
    private readonly IEntityReferenceTypeCompiler entityRefCompiler;

    public CrudApiModule(IEntityDtoCompiler entityDtoCompiler, IEntityReferenceTypeCompiler entityRefCompiler)
    {
        this.entityDtoCompiler = entityDtoCompiler;
        this.entityRefCompiler = entityRefCompiler;
    }

    public override void OnApplyingModule(EntityTypeDefinitionNode node)
    {
        var feature = node.Using<CrudApiFeature>();

        Console.WriteLine($"Compiling {node.DisplayName}");

        feature.DtoType = entityDtoCompiler.Compile(node);
        feature.RefType = entityRefCompiler.Compile(node);
    }
}
