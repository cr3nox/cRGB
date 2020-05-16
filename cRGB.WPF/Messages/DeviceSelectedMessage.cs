﻿namespace cRGB.WPF.Messages
{
    public class DeviceSelectedMessage : IMessage
    {
        public object SelectedDevice { get; set; }

        public DeviceSelectedMessage(object device) => SelectedDevice = device;

        public DeviceSelectedMessage()
        {

        }
    }
}
