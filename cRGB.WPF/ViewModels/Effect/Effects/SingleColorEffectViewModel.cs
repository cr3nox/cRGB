#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Castle.Core.Logging;
using cRGB.Domain.Models.Effect;
using cRGB.Domain.Models.Effect.EffectConfigs;
using cRGB.Domain.Services.System;
using cRGB.WPF.Helpers;
using cRGB.WPF.ViewModels.Device;
using PropertyChanged;
using Serilog;

namespace cRGB.WPF.ViewModels.Effect.Effects
{
    public sealed class SingleColorEffectViewModel : EffectViewModel
    {

        #region Fields

        readonly Random _rng;

        #endregion

        #region Properties

        public ISingleColorLedEffect Cfg => (ISingleColorLedEffect) Config;

        [AlsoNotifyFor(nameof(ColorAsBytes))]
        public Color Color
        {
            get => Cfg.Color;
            set => Cfg.Color = value;
        }

        public byte[] ColorAsBytes => new byte[3] { (byte)Color.R, (byte)Color.G, (byte)Color.B };

        public bool Randomize
        {
            get => Cfg.Randomize;
            set => Cfg.Randomize = value;
        }

        #endregion

        #region ctor

        public SingleColorEffectViewModel(IEventAggregator eventAggregator, ILocalizationHelper loc, ISingleColorLedEffect config) : base(eventAggregator, config)
        {
            DisplayName = loc.GetByKey("SingleColorEffect");
            _rng = new Random();
        }

        #endregion

        #region Methods

        public override async Task<IList<ILedViewModel>> TickAsync(CancellationToken ct, IList<ILedViewModel> leds)
        {
            //Log.Debug("TickAsync");
            return await Task.Run(() => Tick(ct, leds), ct);
        }

        private IList<ILedViewModel> Tick(CancellationToken ct, IList<ILedViewModel> leds)
        {
            int r;
            int g;
            int b;

            if (Randomize)
            {
                r = _rng.Next(0, 255);
                g = _rng.Next(0, 255);
                b = _rng.Next(0, 255);
            }
            else
            {
                r = Color.R;
                g = Color.G;
                b = Color.B;
            }

            foreach (var led in leds)
            {
                ct.ThrowIfCancellationRequested();
                led.SetLedColors(r, g, b);
            }

            return leds;
        }

        public async Task<byte[]> TickAsync(CancellationToken ct, int ledCount)
        {
            return await Task.Run(() => Tick(ct, ledCount), ct);
        }

        private byte[] Tick(CancellationToken ct, int ledCount)
        {
            byte r;
            byte g;

            byte b;

            if (Randomize)
            {
                r = (byte)_rng.Next(0, 255);
                g = (byte)_rng.Next(0, 255);
                b = (byte)_rng.Next(0, 255);
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
                ct.ThrowIfCancellationRequested();
                leds[i] = r;
                leds[i + 1] = g;
                leds[i + 2] = b;
            }

            return leds;
        }

        #endregion
    }
}