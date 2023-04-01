// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection.Emit;
using Clearly.Core;
using Clearly.Crud.EntityGraph;
namespace Clearly.Crud.RestApi;

public class EntityDtoCompiler : ObjectCompiler, IEntityDtoCompiler
{
    public EntityDtoCompiler()
        : base("Clearly.Crud.DynamicDto")
    {
    }

    protected override string GetTypeName(ObjectTypeDefinitionNode typeDefinition)
    {
        return typeDefinition.Type.Name + "__RestDto";
    }

    protected override void HandleProperty(PropertyDefinitionNode property, TypeBuilder typeBuilder)
    {
        if (property.Property.PropertyType.IsAssignableTo(typeof(IEntity)))
        {
            CreateProperty(typeBuilder, property.Property.Name, typeof(Guid));
        }
        else if (property.Property.PropertyType.IsAssignableTo(typeof(IEnumerable<IEntity>)))
        {
            CreateProperty(typeBuilder, property.Property.Name, typeof(IEnumerable<Guid>));
        }
        else
        {
            CreateProperty(typeBuilder, property.Property.Name, property.Property.PropertyType);
        }
    }
}
