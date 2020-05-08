using System.IO;
using System.Threading.Tasks;

namespace cRGB.Domain.Services.System
{
    public interface IXmlSerializationService
    {
        public string Serialize<T>(T content);

        //public T Deserialize<T>(string xml);

        public Task SerializeToFileAsync<T>(T content, string destPath);

        public void SerializeToFile<T>(T content, string destPath);

        public T DeserializeFromFile<T>(string srcPath);
    }
}
