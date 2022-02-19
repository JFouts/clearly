// using System.Linq.Expressions;
// using System.Reflection;
// using AutoMapper;
// using DomainModeling.Core;
// using DomainModeling.Crud.WebUi.Utilities;

// namespace DomainModeling.Crud.WebUi;

// public class EntityPropertyEditorDefinitionBuilder<TEntity, TProp> : EntityEditorDefinitionBuilder<TEntity> where TEntity : IEntity
// {
//     private readonly IMapper _mapper;
//     private readonly EntityEditorDefinitionBuilder<TEntity> _parentBuilder;
//     private readonly EntityEditorDefinitionMutable _entityDefinition;
//     private readonly PropertyInfo _property;

//     internal EntityPropertyEditorDefinitionBuilder(EntityEditorDefinitionBuilder<TEntity> parentBuilder, EntityEditorDefinitionMutable entityDefinition, PropertyInfo property, IMapper mapper)
//         : base(parentBuilder, entityDefinition, mapper)
//     {
//         _parentBuilder = parentBuilder;
//         _entityDefinition = entityDefinition;
//         _property = property;
//         _mapper = mapper;
//     }

//     public virtual FieldEditorDefinitionBuilder<TEntity, TProp> HasFieldEditor(string type)
//     {
//         var field = GetOrCreate();
//         field.FieldType.FieldEditorName = type;

//         return new FieldEditorDefinitionBuilder<TEntity, TProp>(this, _entityDefinition, _property, field, _mapper);
//     }

//     public override EntityPropertyEditorDefinitionBuilder<TEntity, TNextProp> Property<TNextProp>(Expression<Func<TEntity, TNextProp>> propertySelector)
//     {
//         return _parentBuilder.Property(propertySelector);
//     }

    
//     public override EntityEditorDefinitionBuilder<TEntity>WithNameKey(string key)
//     {
//         return _parentBuilder.WithNameKey(key);
//     }

//     public override EntityEditorDefinitionBuilder<TEntity>WithDisplayName(string name)
//     {
//         GetOrCreate().DisplayName = name;

//         return this;
//     }

//     private EntityFieldEditorDefinitionMutable GetOrCreate()
//     {
//         var field = _entityDefinition.Fields.FirstOrDefault(x => x.FieldName == _property.Name);

//         if (field == null)
//         {
//             field = new EntityFieldEditorDefinitionMutable
//             {
//                 FieldName = _property.Name,
//                 DisplayName = _property.Name.SplitCamelCase()
//             };

//             _entityDefinition.Fields.Add(field);
//         }

//         return field;
//     }
// }
