using AutoMapper;
using DomainModeling.Crud;
using DomainModeling.Crud.Services;
using DomainModeling.Crud.WebUi;
using KingdomDeathTools.Api.Services;

namespace KingdomDeathTools.Api.Startup;

// public class AdminEditorDefinition : AdminEditorEntityModule
// {
//     public AdminEditorDefinition(IMapper mapper) : base(mapper)
//     {
//     }

//     public override void OnModelCreating(EditorDefinitionBuilder builder)
//     {
//         builder.Entity<Settlement>()
//             .Property(x => x.Name)
//                 .HasFieldEditor(SystemViewComponents.Input)
//             .Property(x => x.Campaign)
//                 .HasDropDownFieldEditor("People of the Lantern;People of the Sun;People of the Stars")
//             .Property(x => x.Expansions)
//                 .HasFieldEditor(SystemViewComponents.MultiSelectList);

//         builder.Entity<Survivor>()
//             .Property(x => x.SettlementId)
//                 .HasDropDownFieldEditor<Survivor, Guid, EntityDataSource<Settlement>>();
//     }
// }
