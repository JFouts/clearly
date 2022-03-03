using DomainModeling.Core;
using DomainModeling.Crud;
using DomainModeling.Crud.Services;
using DomainModeling.Crud.WebUi;

namespace KingdomDeathTools.Api.Services;

public record Survivor : AggregateRoot, INamedEntity
{
    [FieldEditor(SystemViewComponents.DropDownList)]
    [FieldEditorProperty("DataSource", typeof(EntityDataSource<Settlement>))]
    public Guid SettlementId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Insanity { get; set; }
    public int Evasion { get; set; }
    public int Strength { get; set; }
    public int Accuracy { get; set; }
    public int Luck { get; set; }
}
