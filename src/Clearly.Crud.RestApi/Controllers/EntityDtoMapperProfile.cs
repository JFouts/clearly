// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using AutoMapper;
using Clearly.Core;
using Clearly.Crud.EntityGraph;
namespace Clearly.Crud.RestApi;

public class EntityDtoMapperProfile : Profile
{
    private class EntityIdConverter : IValueConverter<EntityReference, Guid>
    {
        public Guid Convert(EntityReference sourceMember, ResolutionContext context)
        {
            return sourceMember.Id;
        }
    }

    private class EntityCollectionIdConverter : IValueConverter<IEnumerable<EntityReference>, IEnumerable<Guid>>
    {
        public IEnumerable<Guid> Convert(IEnumerable<EntityReference> sourceMember, ResolutionContext context)
        {
            return sourceMember.Select(x => x.Id);
        }
    }

    private class EntityIdReverseConverter : IValueConverter<Guid, EntityReference>
    {
        public EntityReference Convert(Guid sourceMember, ResolutionContext context)
        {
            return new EntityReference { Id = sourceMember };
        }
    }

    private class EntityCollectionIdReverseConverter : IValueConverter<IEnumerable<Guid>, IEnumerable<EntityReference>>
    {
        public IEnumerable<EntityReference> Convert(IEnumerable<Guid> sourceMember, ResolutionContext context)
        {
            return sourceMember.Select(x => new EntityReference { Id = x });
        }
    }

    public EntityDtoMapperProfile(ITypeProvider typeProvider, IEntityDefinitionGraphFactory definitionGraphFactory)
    {
        var typeDefinitions = typeProvider.GetTypes().Select(x => definitionGraphFactory.CreateForType(x));


        foreach (var typeDefinition in typeDefinitions)
        {
            var feature = typeDefinition.Using<CrudApiFeature>();

            if (feature.DtoType == null)
            {
                // TODO: Better Exceptions
                throw new Exception($"Not CRUD API DTO Type is defined for {typeDefinition.NodeKey}!");
            }

            var map = CreateMap(feature.DtoType, feature.RefType);
            var reverseMap = map.ReverseMap();

            foreach (var property in typeDefinition.Properties)
            {
                var propertyName = property.Property.Name;
                if (property.Property.PropertyType.IsAssignableTo(typeof(EntityReference<>)))
                {
                    map.ForMember(propertyName, x => x.Ignore());
                    reverseMap.ForMember(propertyName, x => x.ConvertUsing<Guid, EntityReference>(new EntityIdReverseConverter(), propertyName));
                    reverseMap.ForMember(propertyName, x => x.ConvertUsing<EntityReference, Guid>(new EntityIdConverter(), propertyName));
                }
                else if (property.Property.PropertyType.IsAssignableTo(typeof(IEnumerable<EntityReference>)))
                {
                    map.ForMember(propertyName, x => x.Ignore());
                    reverseMap.ForMember(propertyName, x => x.ConvertUsing<IEnumerable<Guid>, IEnumerable<EntityReference>>(new EntityCollectionIdReverseConverter(), propertyName));
                    reverseMap.ForMember(propertyName, x => x.ConvertUsing<IEnumerable<EntityReference>, IEnumerable<Guid>>(new EntityCollectionIdConverter(), propertyName));
                }
            }
        }
    }
}
