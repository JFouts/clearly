using System.Text;
using DomainModeling.Core.Utilities.Interfaces;

namespace DomainModeling.Core.Utilities
{
    public class Utf32BinaryStringConverter : IBinaryStringConverter
    {
        public byte[] Encode(string str)
        {
            return Encoding.UTF32.GetBytes(str);
        }

        public string Decode(byte[] data)
        {
            return Encoding.UTF32.GetString(data);
        }
    }
}