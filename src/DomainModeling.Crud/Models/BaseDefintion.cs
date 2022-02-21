namespace DomainModeling.Crud;

public abstract class BaseDefinition
{
    protected List<IMetadata> _metadata = new List<IMetadata>();

    public TMetadata UsingMetadata<TMetadata>() where TMetadata : class, IMetadata, new()
    {
        var metadata = _metadata.OfType<TMetadata>().SingleOrDefault();

        if (metadata == null)
        {
            metadata = new TMetadata();
            _metadata.Add(metadata);
        }

        return metadata;
    }
}
