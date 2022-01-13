using DomainModeling.Core;

namespace KingdomDeathTools.Api.Services {
    public class SettlementAggregate : AggregateRoot {
        public string SettlementName { get; set; } = string.Empty;
    }
}