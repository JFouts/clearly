// using System.Reflection;
// using AutoMapper;
// using DomainModeling.Core;

// namespace DomainModeling.Crud.WebUi;

// public class FieldEditorDefinitionBuilder<TEntity, TProp> : EntityPropertyEditorDefinitionBuilder<TEntity, TProp> where TEntity : IEntity
// {
//     private readonly IMapper _mapper;
//     private readonly EntityPropertyEditorDefinitionBuilder<TEntity, TProp> _parentBuilder;
//     private readonly EntityEditorDefinitionMutable _entityDefinition;
//     private readonly PropertyInfo _property;
//     private readonly EntityFieldEditorDefinitionMutable _field;

//     internal FieldEditorDefinitionBuilder(EntityPropertyEditorDefinitionBuilder<TEntity, TProp> parentBuilder, EntityEditorDefinitionMutable entityDefinition, PropertyInfo property, EntityFieldEditorDefinitionMutable field, IMapper mapper)
//         : base(parentBuilder, entityDefinition, property, mapper)
//     {
//         _parentBuilder = parentBuilder;
//         _entityDefinition = entityDefinition;
//         _property = property;
//         _field = field;
//         _mapper = mapper;
//     }

//     public FieldEditorDefinitionBuilder<TEntity, TProp> WithProperty(string key, object value)
//     {
//         _field.FieldType.Properties[key] = value;

//         return this;
//     }

//     public override FieldEditorDefinitionBuilder<TEntity, TProp> HasFieldEditor(string type)
//     {
//         return _parentBuilder.HasFieldEditor(type);
//     }
// }
