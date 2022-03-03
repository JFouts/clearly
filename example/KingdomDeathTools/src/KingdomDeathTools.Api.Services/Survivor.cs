using Clearly.Core;
using Clearly.Crud;
using Clearly.Crud.Services;
using Clearly.Crud.WebUi;

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
