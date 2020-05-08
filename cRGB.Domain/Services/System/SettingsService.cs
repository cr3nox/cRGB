#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Collections.Generic;
using System.IO;
using System.Linq;
using cRGB.Domain.Models.Device;
using cRGB.Domain.Models.System;

namespace cRGB.Domain.Services.System
{
    public class SettingsService : ISettingsService
    {
        #region Fields

        readonly IJsonSerializationService _jsonSerializationService;
        string SettingsPath { get; }
        private string CurrentDirectory { get; }

        #endregion

        #region Properties
        public ISettings Settings { get; set; }

        #endregion
        
        #region ctor
        public SettingsService(IJsonSerializationService jsonSerializationService)
        {
            CurrentDirectory = Directory.GetCurrentDirectory();
            SettingsPath = Path.Combine(CurrentDirectory, "AppConfig.json");
            _jsonSerializationService = jsonSerializationService;
            LoadAppSettingsFromFile();
        }

        #endregion

        #region Methods
        public void LoadAll()
        {
            //LoadAppSettingsFromFile();
            //LoadBlinkStickSettingsFromFile();
            EnsureAppSettingsExist();
            var x = _jsonSerializationService.DeserializeFromFile<Settings>(SettingsPath);

        }

        public void SaveAll()
        {
            //SaveAppSettingsToFile();
            //SaveBlinkStickSettingsToFile();
            _jsonSerializationService.SerializeToFile(Settings, SettingsPath);
        }


        public IBlinkStickSettings RegisterBlinkStickSettings(string serial)
        {
            if (string.IsNullOrEmpty(serial))
                return null;

            var stick = GetBlinkStickSettings(serial);
            if(stick != null)
                return stick;

            stick = new BlinkStickSettings(serial);
            Settings.ConfiguredDevices.Add((stick));
            return stick;
        }

        public IBlinkStickSettings GetBlinkStickSettings(string serial)
        {
            return Settings.ConfiguredDevices.OfType<IBlinkStickSettings>().FirstOrDefault(bSet => bSet.SerialNumber == serial);
        }

        public void LoadAppSettingsFromFile()
        {
            //EnsureAppSettingsExist();
            //AppSettings = _jsonSerializationService.DeserializeFromFile<AppSettings>(AppSettingsPath);
        }

        void LoadBlinkStickSettingsFromFile()
        {
            //if (!File.Exists(BlinkStickSettingsPath))
            //{
            //    BlinkStickSettings = new List<IBlinkStickSettings>();
            //    return;
            //}
            //BlinkStickSettings = _jsonSerializationService.DeserializeFromFile<List<BlinkStickSettings>>(BlinkStickSettingsPath).ToList<IBlinkStickSettings>();
        }

        void SaveBlinkStickSettingsToFile()
        {
            //_jsonSerializationService.SerializeToFile(BlinkStickSettings, BlinkStickSettingsPath);
        }


        public void SaveAppSettingsToFile()
        {
            //_jsonSerializationService.SerializeToFile(AppSettings, AppSettingsPath);
        }

        public void EnsureAppSettingsExist()
        {
            if (File.Exists(SettingsPath))
                return;

            Settings = new Settings();
            _jsonSerializationService.SerializeToFile(Settings, SettingsPath);
        }

        #endregion
    }
}