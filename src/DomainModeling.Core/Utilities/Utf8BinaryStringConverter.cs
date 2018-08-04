using System.Text;
using DomainModeling.Core.Utilities.Interfaces;

namespace DomainModeling.Core.Utilities {
    public class Utf8BinaryStringConverter : IBinaryStringConverter {
        public byte[] Encode(string str) => Encoding.UTF8.GetBytes(str);
        public string Decode(byte[] data) => Encoding.UTF8.GetString(data);
    }
}