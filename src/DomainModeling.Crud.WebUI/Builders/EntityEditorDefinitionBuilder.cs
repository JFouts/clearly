// using System.Linq.Expressions;
// using AutoMapper;
// using DomainModeling.Core;

// namespace DomainModeling.Crud.WebUi;

// public class EntityEditorDefinitionBuilder<TEntity> : EditorDefinitionBuilder where TEntity : IEntity
// {
//     private readonly IMapper _mapper;
//     private readonly EditorDefinitionBuilder _parentBuilder;
//     private readonly EntityEditorDefinitionMutable _entityDefinition;

//     internal EntityEditorDefinitionBuilder(EditorDefinitionBuilder parentBuilder, EntityEditorDefinitionMutable entityDefinition, IMapper mapper)
//         : base(mapper)
//     {
//         _parentBuilder = parentBuilder;
//         _entityDefinition = entityDefinition;
//         _mapper = mapper;
//     }

//     public virtual EntityPropertyEditorDefinitionBuilder<TEntity, TProp> Property<TProp>(Expression<Func<TEntity, TProp>> propertySelector)
//     {
//         var propertyInfo = propertySelector.GetPropertyInfo();

//         return new EntityPropertyEditorDefinitionBuilder<TEntity, TProp>(this, _entityDefinition, propertyInfo, _mapper);
//     }

//     public virtual EntityEditorDefinitionBuilder<TEntity>WithNameKey(string key)
//     {
//         _entityDefinition.NameKey = key;

//         return this;
//     }

//     public virtual EntityEditorDefinitionBuilder<TEntity>WithDisplayName(string name)
//     {
//         _entityDefinition.DisplayName = name;

//         return this;
//     }

//     public override EntityEditorDefinitionBuilder<TNextEntity> Entity<TNextEntity>()
//     {
//         return _parentBuilder.Entity<TNextEntity>();
//     }
// }
