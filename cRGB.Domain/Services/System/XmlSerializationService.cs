using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace cRGB.Domain.Services.System
{
    public class XmlSerializationService : IXmlSerializationService
    {
        //public T Deserialize<T>(string xml)
        //{
        //    var serializer = new XmlSerializer(typeof(string));
        //    return null;
        //}

        public Task SerializeToFileAsync<T>(T content, string destPath)
        {
            throw new NotImplementedException();
        }

        public void SerializeToFile<T>(T content, string destPath)
        {
            throw new NotImplementedException();
        }

        public T DeserializeFromFile<T>(string srcPath)
        {
            throw new NotImplementedException();
        }

        string IXmlSerializationService.Serialize<T>(T content)
        {
            throw new NotImplementedException();
        }
    }
}


