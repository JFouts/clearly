// using AutoMapper;

// namespace DomainModeling.Crud.WebUi;

// public abstract class AdminEditorEntityModule : EntityModule
// {
//     protected readonly IMapper _mapper;

//     protected AdminEditorEntityModule(IMapper mapper)
//     {
//         _mapper = mapper;
//     }

//     public abstract void OnModelCreating(EditorDefinitionBuilder builder);

//     public override void OnApplyingModule(EntityDefinition entity)
//     {
//         var Definition = GetOrBuildEditorDefinition();

//         if (Definition.Entities.ContainsKey(entity.Entity))
//         {
//             var metadata = entity.UsingMetadata<CrudAdminEntityMetadata>();

//             metadata.EditorDefinition = Definition.Entities[entity.Entity];
//         }
//     }

//     private EditorDefinitionMutable? _builtDefinition;
//     private readonly object _lock = new object();
//     private EditorDefinitionMutable GetOrBuildEditorDefinition()
//     {
//         SynchronizationHelper.CriticalInitializer(_lock, ref _builtDefinition!, () =>
//         {
//             var builder = new EditorDefinitionBuilder(_mapper);
//             OnModelCreating(builder);

//             return builder.GetMutableDefinition();
//         });

//         return _builtDefinition;
//     }
// }
