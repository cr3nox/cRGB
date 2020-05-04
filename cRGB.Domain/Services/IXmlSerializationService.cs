using System.IO;

namespace cRGB.Domain.Services
{
    public interface IXmlSerializationService
    {
        public Stream Serialize<T>(T content);

        public T Deserialize<T>(Stream stream);
    }
}
