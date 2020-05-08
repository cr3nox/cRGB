#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using cRGB.Domain.Models.Device;
using cRGB.Domain.Models.System;
using Microsoft.VisualBasic;
using Serilog;
using Serilog.Context;

namespace cRGB.Domain.Services.System
{
    public class SettingsService : ISettingsService
    {
        #region Fields

        readonly IJsonSerializationService _jsonSerializationService;
        readonly IXmlSerializationService _xmlSerializationService;
        string SettingsPath { get; }
        private string CurrentDirectory { get; }

        #endregion

        #region Properties
        public ISettings Settings { get; set; }

        #endregion
        
        #region ctor
        public SettingsService(IJsonSerializationService jsonSerializationService, IXmlSerializationService xmlSerializationService)
        {
            CurrentDirectory = Directory.GetCurrentDirectory();
            SettingsPath = Path.Combine(CurrentDirectory, "cRGB_Config.xml");
            _jsonSerializationService = jsonSerializationService;
            _xmlSerializationService = xmlSerializationService;
        }

        #endregion

        #region Methods
        public void LoadAll()
        {
            LoadAppSettings();
        }

        public void SaveAll()
        {
            _xmlSerializationService.SerializeToFile(Settings, SettingsPath);
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

        public void LoadAppSettings()
        {
            if (!File.Exists(SettingsPath))
            {
                Log.Information($"No Configuration File found, creating new one.");
                CreateNewSettingsFile();
            }
            else
            {
                // LogContext Serilog useful when switching to structured logging
                using (LogContext.PushProperty("Settings Path", SettingsPath))
                {
                    // Test if the XML Config File is valid otherwise create new one;
                    try
                    {
                        Settings = _xmlSerializationService.DeserializeFromFile<Settings>(SettingsPath);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Configuration file could not be read. It's either not valid XML or corrupted. Backing up old file and create fresh config File.");
                        BackupOldSettingsFile();
                        CreateNewSettingsFile();
                    }
                }
            }
        }

        private void BackupOldSettingsFile()
        {
            try
            {
                File.Move(SettingsPath, $"cRGB_Config_backup_{DateTime.Now:yyyyMMdd-HH-mm-ss}.xml");
            }
            catch (Exception e)
            {
                Log.Error(e, $"Backup Failed");
            }
        }

        private void CreateNewSettingsFile()
        {
            Settings = new Settings();
            _xmlSerializationService.SerializeToFile(Settings, SettingsPath);
        }

        #endregion
    }
}