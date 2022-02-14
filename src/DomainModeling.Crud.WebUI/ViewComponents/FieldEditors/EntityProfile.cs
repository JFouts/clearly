
using AutoMapper;
using DomainModeling.Core;

namespace DomainModeling.Crud.WebUi.ViewComponents.FieldEditors;

public class EntityProfile : Profile
{
	public EntityProfile()
	{
		CreateMap<INamedEntity, KeyValuePair<string, string>>()
            .ConvertUsing(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
	}
}
