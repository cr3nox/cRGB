#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using Serilog;

namespace cRGB.Domain.Services.System
{
    public interface ILogService
    {
        public ILogger InitLogger();
    }
}