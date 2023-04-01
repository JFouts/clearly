// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection.Emit;
using Clearly.Core;
using Clearly.Crud.EntityGraph;
namespace Clearly.Crud.RestApi;

public class EntityReferenceTypeCompiler : ObjectCompiler, IEntityReferenceTypeCompiler
{
    public EntityReferenceTypeCompiler()
        : base("Clearly.Crud.DynamicDto")
    {
    }

    protected override string GetTypeName(ObjectTypeDefinitionNode typeDefinition)
    {
        return typeDefinition.Type.Name + "__Ref";
    }

    protected override void HandleProperty(PropertyDefinitionNode property, TypeBuilder typeBuilder)
    {
        var propertyType = property.Property.PropertyType;

        if (property.Property.PropertyType.IsAssignableTo(typeof(IEntity)))
        {
            // Convert Data type to EntityReference<TEntity>
            // CreateProperty(typeBuilder, property.Property.Name, typeof(EntityReference<>).MakeGenericType(propertyType));

            // Convert Data type to EntityReference
            CreateProperty(typeBuilder, property.Property.Name, typeof(EntityReference));
        }
        else if (property.Property.PropertyType.IsAssignableTo(typeof(IEnumerable<IEntity>)))
        {
            // Convert Data type to IEnumerable<EntityReference<TEntity>>
            // CreateProperty(typeBuilder, property.Property.Name, typeof(IEnumerable<>)
            //     .MakeGenericType(typeof(EntityReference<>)
            //         .MakeGenericType(propertyType.GenericTypeArguments[0])));

            // Convert Data type to IEnumerable<EntityReference>
            CreateProperty(typeBuilder, property.Property.Name, typeof(IEnumerable<EntityReference>));
        }
        else
        {
            CreateProperty(typeBuilder, property.Property.Name, property.Property.PropertyType);
        }
    }
}
