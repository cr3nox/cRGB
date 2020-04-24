using System;
using System.Drawing;
using System.Net.Mime;
using System.Threading;
using BlinkStickDotNet;

namespace ChannelTest
{
    class Program
    {
        //static void SetColor(BlinkStick device, byte channel, byte r, byte g, byte b)
        //{
        //    byte[] data = new byte[3 * 8];

        //    for (int i = 0; i < data.Length / 3; i++)
        //    {
        //        data[i * 3] = g;
        //        data[i * 3 + 1] = r;
        //        data[i * 3 + 2] = b;
        //    }

        //    device.SetColors(channel, data);
        //    Thread.Sleep(20);
        //}

        static void Main(string[] args)
        {
            BlinkStick device = BlinkStick.FindFirst();

            if (device != null)
            {
                if (device.OpenDevice())
                {
                    var rnd = new Random();
                    Color c1 = Color.FromArgb(0, rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                    Color c2 = Color.FromArgb(0, rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));

                    for (int i = 0; i < 64; i++)
                    {
                        device.SetColor(0, (byte)i, c1.R, c1.G, c1.B);
                        Console.WriteLine($"Set LED {i} to Color {c1}");
                        System.Threading.Thread.Sleep(50);
                    }
                    for (int i = 0; i < 42; i++)
                    {
                        device.SetColor(1, (byte)i, c2.R, c2.G, c2.B);
                        Console.WriteLine($"Set LED {i} to Color {c2}");
                        System.Threading.Thread.Sleep(50);
                    }
                }
                else
                {
                    Console.WriteLine("Could not open the device");
                }
            }
            else
            {
                Console.WriteLine("BlinkStick not found");
            }
        }
    }
}
