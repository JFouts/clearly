namespace DomainModeling.Crud.Services;

public record HostedCrudApiConfiguration
{
    public string BaseUrl { get; set; } = string.Empty;
}
