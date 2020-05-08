using cRGB.Domain.Models.Enums;

namespace cRGB.Domain.Models.Device
{
    public interface ILedDevice
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ELedDeviceType DeviceType { get; set; }
    }
}
