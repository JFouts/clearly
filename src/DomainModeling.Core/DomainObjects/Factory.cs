namespace DomainModeling.Core {
    public interface Factory<T> where T : Entity {
        T Create();
    }
}
