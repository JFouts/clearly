namespace DomainModeling.Core.Utilities.Interfaces {
    public interface IJsonByteSerializer {
        byte[] Serialize(object obj);
    }
}