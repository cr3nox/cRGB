#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using Caliburn.Micro;
using cRGB.Domain.Models.Effect.EffectConfigs;
using cRGB.WPF.Helpers;
using cRGB.WPF.Messages;
using cRGB.WPF.ServiceLocation.Factories;
using cRGB.WPF.ViewModels.Device;
using PropertyChanged;

namespace cRGB.WPF.ViewModels.Effect.Effects
{
    public sealed class SingleColorEffectViewModel : EffectViewModel
    {

        #region Fields

        readonly Random _rng;
        readonly ILedColorFactory _ledColorFactory;

        #endregion

        #region Properties

        public ISingleColorLedEffect Cfg => (ISingleColorLedEffect) Config;

        [AlsoNotifyFor(nameof(ColorAsBytes))]
        public Color EffectColor
        {
            get => Color.FromRgb(Cfg.Color.R, Cfg.Color.G, Cfg.Color.B);
            set => Cfg.Color.SetColor(value.R, value.G, value.B);
        }

        public byte[] ColorAsBytes => new byte[3] { EffectColor.R, EffectColor.G, EffectColor.B };

        public bool Randomize
        {
            get => Cfg.Randomize;
            set => Cfg.Randomize = value;
        }

        #endregion

        #region ctor

        public SingleColorEffectViewModel(IEventAggregator eventAggregator, ILocalizationHelper loc, ISingleColorLedEffect config, ILedColorFactory ledColorFactory) 
            : base(eventAggregator, config)
        {
            DisplayName = loc.GetByKey("SingleColorEffect");
            _rng = new Random();
            _ledColorFactory = ledColorFactory;
            config.Color ??= _ledColorFactory.Create();
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
            var bytes = Tick(ct, leds.Count);
            var index = LedStartIndex;
            for(var i = LedStartIndex * 3; i < bytes.Length; i += 3)
            {
                ct.ThrowIfCancellationRequested();
                leds[index++].SetLedColors(bytes[i], bytes[i+1], bytes[i+2]);
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
                r = EffectColor.R;
                g = EffectColor.G;
                b = EffectColor.B;
            }

            var leds = new byte[ledCount * 3];
            for (var i = LedStartIndex * 3; i < ledCount * 3; i += 3)
            {
                ct.ThrowIfCancellationRequested();
                if (i > LedEndIndex * 3)
                    break;
                leds[i] = r;
                leds[i + 1] = g;
                leds[i + 2] = b;
            }

            return leds;
        }

        public void Test()
        {
            EffectColor = Color.FromArgb(255, (byte)_rng.Next(0, 255), (byte)_rng.Next(0, 255), (byte)_rng.Next(0, 255));
        }

        #endregion
    }
}