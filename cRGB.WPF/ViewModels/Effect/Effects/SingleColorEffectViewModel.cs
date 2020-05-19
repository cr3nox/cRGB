﻿#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Caliburn.Micro;
using Castle.Core.Logging;
using cRGB.Domain.Models.Effect.EffectConfigs;
using cRGB.Domain.Services.System;
using cRGB.WPF.ViewModels.Device;
using PropertyChanged;

namespace cRGB.WPF.ViewModels.Effect.Effects
{
    public sealed class SingleColorEffectViewModel : EffectViewModel
    {

        #region Fields
        
        readonly ISingleColorLedEffect _config;
        readonly Random _rng;
        readonly ILogService _logService;

        #endregion

        #region Properties

        [AlsoNotifyFor(nameof(ColorAsBytes))]
        public Color Color
        {
            get => _config.Color;
            set => _config.Color = value;
        }

        public byte[] ColorAsBytes => new byte[3] { (byte)Color.R, (byte)Color.G, (byte)Color.B };

        public bool Randomize
        {
            get => _config.Randomize;
            set => _config.Randomize = value;
        }

        #endregion

        #region ctor

        public SingleColorEffectViewModel(IEventAggregator eventAggregator, ILogService logService, ISingleColorLedEffect config) : base(eventAggregator)
        {
            _config = config;
            _rng = new Random();
            _logService = logService;
        }

        #endregion

        #region Methods

        public override async Task<byte[]> TickAsync(int ledCount)
        {
            return await Task.Run(() => Tick(ledCount));
        }

        private byte[] Tick(int ledCount)
        {
            byte r;
            byte g;

            byte b;

            if (Randomize)
            {
                r = (byte) _rng.Next(0, 255);
                g = (byte) _rng.Next(0, 255);
                b = (byte) _rng.Next(0, 255);
            }

            else
            {
                r = Color.R;
                g = Color.G;
                b = Color.B;
            }

            var leds = new byte[ledCount * 3];
            for (var i = 0; i < ledCount * 3; i += 3)
            {
                leds[i] = r;
                leds[i + 1] = g;
                leds[i + 2] = b;
            }

            return leds;
        }

        #endregion
    }
}