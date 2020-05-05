#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Threading.Tasks;

namespace cRGB.Domain.Services
{
    public interface IJsonSerializationService
    {
        public string Serialize<T>(T content);

        public T Deserialize<T>(string json);

        public Task SerializeToFileAsync<T>(T content, string destPath);

        public void SerializeToFile<T>(T content, string destPath);

        public T DeserializeFromFile<T>(string srcPath);
    }
}