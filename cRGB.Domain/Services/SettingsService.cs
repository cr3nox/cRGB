#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Collections.Generic;
using cRGB.Domain.Models.App;
using cRGB.Domain.Models.Device;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;

namespace cRGB.Domain.Services
{
    public class SettingsService : ISettingsService
    {
        #region Fields

        readonly IJsonSerializationService _jsonSerializationService;
        string AppSettingsPath { get; }
        string BlinkStickSettingsPath { get; }
        private string CurrentDirectory { get; }

        #endregion

        #region Properties
        public IList<IBlinkStickSettings> BlinkStickSettings { get; set; }
        public IAppSettings AppSettings { get; set; }

        #endregion
        
        #region ctor
        public SettingsService(IJsonSerializationService jsonSerializationService)
        {
            CurrentDirectory = Directory.GetCurrentDirectory();
            AppSettingsPath = Path.Combine(CurrentDirectory, "AppConfig.json");
            BlinkStickSettingsPath = Path.Combine(CurrentDirectory, "BlinkStickDevices.json");
            _jsonSerializationService = jsonSerializationService;
            LoadAppSettingsFromFile();
        }

        #endregion

        #region Methods
        public void LoadAll()
        {
            LoadAppSettingsFromFile();
            LoadBlinkStickSettingsFromFile();
        }

        public void SaveAll()
        {
            SaveAppSettingsToFile();
            SaveBlinkStickSettingsToFile();
        }


        public IBlinkStickSettings RegisterBlinkStickSettings(string serial)
        {
            if (string.IsNullOrEmpty(serial))
                return null;

            var stick = GetBlinkStickSettings(serial);
            if(stick != null)
                return stick;

            stick = new BlinkStickSettings(serial);
            BlinkStickSettings.Add(stick);
            return stick;
        }

        public IBlinkStickSettings GetBlinkStickSettings(string serial)
        {
            return BlinkStickSettings.SingleOrDefault(o => o.SerialNumber == serial);
        }

        public void LoadAppSettingsFromFile()
        {
            EnsureAppSettingsExist();
            AppSettings = _jsonSerializationService.DeserializeFromFile<AppSettings>(AppSettingsPath);
        }

        void LoadBlinkStickSettingsFromFile()
        {
            if (!File.Exists(BlinkStickSettingsPath))
            {
                BlinkStickSettings = new List<IBlinkStickSettings>();
                return;
            }
            BlinkStickSettings = _jsonSerializationService.DeserializeFromFile<List<BlinkStickSettings>>(BlinkStickSettingsPath).ToList<IBlinkStickSettings>();
        }

        void SaveBlinkStickSettingsToFile()
        {
            _jsonSerializationService.SerializeToFile(BlinkStickSettings, BlinkStickSettingsPath);
        }


        public void SaveAppSettingsToFile()
        {
            _jsonSerializationService.SerializeToFile(AppSettings, AppSettingsPath);
        }

        public void EnsureAppSettingsExist()
        {
            if (File.Exists(AppSettingsPath))
                return;

            AppSettings = new AppSettings();
            _jsonSerializationService.SerializeToFile(AppSettings, AppSettingsPath);
        }

        #endregion
    }
}