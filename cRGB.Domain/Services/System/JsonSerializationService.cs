#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace cRGB.Domain.Services.System
{
    public class JsonSerializationService : IJsonSerializationService
    {
        public string Serialize<T>(T content)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            return JsonSerializer.Serialize<T>(content);
        }

        public T Deserialize<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
                throw new ArgumentNullException(nameof(json));

            return JsonSerializer.Deserialize<T>(json);
        }

        public async Task SerializeToFileAsync<T>(T content, string destPath)
        {
            await File.WriteAllTextAsync(destPath, Serialize(content));
        }

        public void SerializeToFile<T>(T content, string destPath)
        {
            File.WriteAllText(destPath, Serialize(content));
        }

        public T DeserializeFromFile<T>(string srcPath)
        {
            var file = File.ReadAllText(srcPath);
            return Deserialize<T>(file);
        }

        //private string CorrectFileExtension(string path)
        //{
        //    return Path.GetExtension(path).EndsWith("json") ? path : Path.ChangeExtension(path, "json");
        //}
    }
}