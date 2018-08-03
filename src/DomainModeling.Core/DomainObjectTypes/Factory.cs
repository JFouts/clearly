namespace DomainModeling.Core.DomainObjectTypes {
    public interface Factory<T> where T : Entity {
        T Create();
    }
}
