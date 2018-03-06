using DomainModeling.Core.Utilities;
using DomainModeling.Core.Utilities.Interfaces;
using Moq;
using Xunit;

namespace DomainModeling.Core.Unit.Utilities
{
    public class JsonByteConverterTests
    {
        private JsonByteConverter _converter;
        private Mock<IJsonConverter> _jsonConverter;
        private Mock<IBinaryStringConverter> _byteConverter;

        public JsonByteConverterTests()
        {
            _jsonConverter = new Mock<IJsonConverter>();
            _byteConverter = new Mock<IBinaryStringConverter>();

            _converter = new JsonByteConverter(_jsonConverter.Object, _byteConverter.Object);
        }

        [Fact]
        public void WhenSerializingItCovertToJsonThenBytes()
        {
            // Arrange
            var obj = new {};
            var json = "test";
            var expected = new byte[10];

            _jsonConverter.Setup(x => x.Serialize(obj)).Returns(json);
            _byteConverter.Setup(x => x.Encode(json)).Returns(expected);

            // Act
            var result = _converter.Serialize(obj);

            // Assert
            Assert.Same(expected, result);
        }

        [Fact]
        public void WhenDeserializingToObjectItCovertFromBytesThenFromJson()
        {
            // Arrange
            var bytes = new byte[10];
            var json = "test";
            var expected = new { };

            _byteConverter.Setup(x => x.Decode(bytes)).Returns(json);
            _jsonConverter.Setup(x => x.Deserialize(json)).Returns(expected);

            // Act
            var result = _converter.Deserialize(bytes);

            // Assert
            Assert.Same(expected, result);
        }

        [Fact]
        public void WhenDeserializingToTypeItCovertFromBytesThenFromJson()
        {
            // Arrange
            var bytes = new byte[10];
            var json = "test";
            var expected = new object();

            _byteConverter.Setup(x => x.Decode(bytes)).Returns(json);
            _jsonConverter.Setup(x => x.Deserialize(json, typeof(object))).Returns(expected);

            // Act
            var result = _converter.Deserialize(bytes, expected.GetType());

            // Assert
            Assert.Same(expected, result);
        }

        [Fact]
        public void WhenDeserializingToExplicitTypeItCovertFromBytesThenFromJson()
        {
            // Arrange
            var bytes = new byte[10];
            var json = "test";
            var expected = new object();

            _byteConverter.Setup(x => x.Decode(bytes)).Returns(json);
            _jsonConverter.Setup(x => x.Deserialize<object>(json)).Returns(expected);

            // Act
            var result = _converter.Deserialize<object>(bytes);

            // Assert
            Assert.Same(expected, result);
        }
    }
}
