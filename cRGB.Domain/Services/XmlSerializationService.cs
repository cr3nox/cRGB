using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace cRGB.Domain.Services
{
    public class XmlSerializationService : IXmlSerializationService

    {
        public Stream Serialize<T>(T content)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            var settings = new XmlWriterSettings
            {
                Indent = false,
                NewLineHandling = NewLineHandling.None
            };

            var serializer = new XmlSerializer(typeof(T));

            var ms = new MemoryStream();
            using (var sw = new StreamWriter(ms, Encoding.UTF8, 1024, true))
            {
                using var xmlWriter = XmlWriter.Create(sw, settings);
                serializer.Serialize(xmlWriter, content);
                xmlWriter.Flush();
            }

            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        public T Deserialize<T>(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            using (stream)
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T) serializer.Deserialize(XmlReader.Create(stream));
            }
        }

        public void CopyStream(Stream stream, string destPath = "")
        {
            using var fileStream = new FileStream(destPath, FileMode.OpenOrCreate, FileAccess.Write);
            stream.CopyTo(fileStream);
        }


    }
}


