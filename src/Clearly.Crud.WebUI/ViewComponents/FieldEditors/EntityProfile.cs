// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using AutoMapper;
using Clearly.Core;

namespace Clearly.Crud.WebUi.ViewComponents.FieldEditors;

public class EntityProfile : Profile
{
    public EntityProfile()
    {
        CreateMap<INamedEntity, KeyValuePair<string, string>>()
            .ConvertUsing(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
    }
}

public static class AutoMapperExtensions
{
    public static IMappingExpression<T1, T2> ConstructorFromProperties<T1, T2>(this IMappingExpression<T1, T2> mapper)
    {
        var constructor = typeof(T2).GetConstructors().First();

        foreach (var param in constructor.GetParameters())
        {
            mapper = mapper.ForCtorParam(param.Name, opt => opt.MapFrom(param.Name!.ToPascalCase()));
        }

        return mapper;
    }
}
