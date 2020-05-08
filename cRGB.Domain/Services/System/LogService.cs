#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using Serilog;

namespace cRGB.Domain.Services.System
{
    public class LogService : ILogService
    {
        public ILogger InitLogger()
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
                .WriteTo.Debug()
#endif
                .WriteTo.File("cRGB_.log", rollingInterval: RollingInterval.Day, shared: true)
                .CreateLogger();
            return Log.Logger;
        }
        
    }
}