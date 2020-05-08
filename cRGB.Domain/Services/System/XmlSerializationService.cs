using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using cRGB.Domain.Models.System;
using ExtendedXmlSerializer;
using ExtendedXmlSerializer.Configuration;

namespace cRGB.Domain.Services.System
{
    public class XmlSerializationService : IXmlSerializationService
    {
        #region Serializing
        public string Serialize<T>(T content)
        {
            return GetSerializer<T>().Serialize(content);
        }

        public void SerializeToFile<T>(T content, string destPath)
        {
            File.WriteAllText(destPath, Serialize(content));
        }

        #endregion

        #region Deserialize
        public T Deserialize<T>(string xml)
        {
            return GetSerializer<T>().Deserialize<T>(new XmlReaderSettings { IgnoreWhitespace = false }, xml);
        }

        public T DeserializeFromFile<T>(string srcPath)
        {
            return Deserialize<T>(File.ReadAllText(srcPath));
        }

        #endregion

        private static IExtendedXmlSerializer GetSerializer<T>()
        {
            return new ConfigurationContainer()
                .Type<T>()
                .Create();
        }
    }
}


