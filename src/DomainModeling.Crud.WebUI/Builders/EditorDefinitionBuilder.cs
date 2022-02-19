// using AutoMapper;
// using DomainModeling.Core;
// using DomainModeling.Crud.WebUi.Models;
// using DomainModeling.Crud.WebUi.Utilities;

// namespace DomainModeling.Crud.WebUi;

// /// <summary>
// /// A builder class for creating an <see cref="EditorDefinition" />
// /// </summary>
// public class EditorDefinitionBuilder
// {
//     private string? Definition;
//     private readonly IMapper _mapper;
//     private readonly EditorDefinitionMutable _definition = new EditorDefinitionMutable();

//     internal EditorDefinitionBuilder(IMapper mapper)
//     {
//         _mapper = mapper;
//     }

//     /// <summary>
//     /// Selects an entity to further define the properties of the <see cref="EditorDefinition" />.
//     /// </summary>
//     /// <typeparam name="TEntity">The Entity to select</typeparam>
//     /// <returns>A sub-builder for the continued configuration of the selected Entity in the <see cref="EditorDefinition" />.</returns>
//     public virtual EntityEditorDefinitionBuilder<TEntity> Entity<TEntity>() where TEntity : IEntity
//     {
//         var type = typeof(TEntity);
        
//         if (!_definition.Entities.ContainsKey(type))
//         {
//             _definition.Entities[type] = new EntityEditorDefinitionMutable {
//                 NameKey = type.FullName!,
//                 DisplayName = type.Name.SplitCamelCase(),
//                 DataSourceUrl = $"/api/{type.Name.ToLower()}"
//             };
//         }

//         var entityDefinition = _definition.Entities[type];

//         return new EntityEditorDefinitionBuilder<TEntity>(this, entityDefinition, _mapper);
//     }

//     /// <summary>
//     /// Generates the configured EditorDefinition
//     /// </summary>
//     /// <returns></returns>
//     public EditorDefinition Create()
//     {
//         return _mapper.Map<EditorDefinition>(_definition);
//     }

//     internal EditorDefinitionMutable GetMutableDefinition()
//     {
//         return _definition;
//     }
// }
