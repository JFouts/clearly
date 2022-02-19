// using DomainModeling.Core;
// using DomainModeling.Crud.Services;

// namespace DomainModeling.Crud.WebUi;

// public static class EditorDefinitionBuilderExtensions
// {
//     /// <summary>
//     /// Sets the field to use a Drop Down editor when editing this field and populates that drop down with a static
//     /// list of options.
//     /// </summary>
//     /// <param name="staticOptions">A semicolon separated list of static options to populate the dropdown list.</param>
//     public static FieldEditorDefinitionBuilder<TEntity, TProp> HasDropDownFieldEditor<TEntity, TProp>(this EntityPropertyEditorDefinitionBuilder<TEntity, TProp> builder,  string staticOptions) where TEntity : IEntity
//     {
//         return builder
//             .HasFieldEditor(SystemViewComponents.DropDownList)
//             .WithProperty("DataSource", staticOptions);
//     }

//     /// <summary>
//     /// Sets the field to use a Drop Down editor when editing this field and populates that drop down with the passed
//     /// data source.
//     /// </summary>
//     public static FieldEditorDefinitionBuilder<TEntity, TProp> HasDropDownFieldEditor<TEntity, TProp, TDataSource>(this EntityPropertyEditorDefinitionBuilder<TEntity, TProp> builder) 
//         where TEntity : IEntity 
//         where TDataSource : IDataSource
//     {
//         return builder.HasDropDownFieldEditor(typeof(TDataSource));
//     }

//     /// <summary>
//     /// Sets the field to use a Drop Down editor when editing this field and populates that drop down with the passed
//     /// data source.
//     /// </summary>
//     /// <param name="dataSource">The type of the data source used to populate the drop down list.</param>
//     public static FieldEditorDefinitionBuilder<TEntity, TProp> HasDropDownFieldEditor<TEntity, TProp>(this EntityPropertyEditorDefinitionBuilder<TEntity, TProp> builder, Type dataSource) 
//         where TEntity : IEntity
//     {
//         return builder
//             .HasFieldEditor(SystemViewComponents.DropDownList)
//             .WithProperty("DataSource", dataSource);
//     }
// }