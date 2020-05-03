using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cRGB.Domain.Services
{
    public interface IXmlSerializationService
    {
        public Stream Serialize<T>(T content);

        public T Deserialize<T>(Stream stream);
    }
}
