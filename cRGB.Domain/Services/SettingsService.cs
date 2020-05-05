#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Collections.Generic;
using cRGB.Domain.Models.App;
using cRGB.Domain.Models.Device;
using System.IO;
using System.Runtime.Serialization.Formatters;

namespace cRGB.Domain.Services
{
    public class SettingsService : ISettingsService
    {
        #region Fields

        readonly IJsonSerializationService _jsonSerializationService;
        string AppSettingsFileName { get; } = "AppConfig.json";
        private string CurrentDirectory { get; }

        #endregion

        #region Properties
        public IEnumerable<IBlinkStickSettings> BlinkStickSettings { get; set; } = new List<IBlinkStickSettings>();
        public IAppSettings AppSettings { get; set; }

        #endregion
        
        #region ctor
        public SettingsService(IJsonSerializationService jsonSerializationService)
        {
            CurrentDirectory = Directory.GetCurrentDirectory();
            _jsonSerializationService = jsonSerializationService;
            LoadAppSettingsFromFile();
        }

        #endregion

        #region Methods

        public void LoadAppSettingsFromFile()
        {
            EnsureAppSettingsExist();
            AppSettings = _jsonSerializationService.DeserializeFromFile<AppSettings>(Path.Combine(CurrentDirectory, AppSettingsFileName));
        }

        public void SaveAppSettingsToFile()
        {
            _jsonSerializationService.SerializeToFile(AppSettings, Path.Combine(CurrentDirectory, AppSettingsFileName));
        }

        public void EnsureAppSettingsExist()
        {
            var path = Path.Combine(CurrentDirectory, AppSettingsFileName);
            if (File.Exists(path))
                return;

            AppSettings = new AppSettings();
            _jsonSerializationService.SerializeToFile(AppSettings, path);
        }

        public void SaveAll()
        {
            SaveAppSettingsToFile();
        }

        #endregion
    }
}