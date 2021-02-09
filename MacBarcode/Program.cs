using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;

namespace MacBarcode
{
    class Program
    {
        static void Main(string[] args)
        {
            String macAddr = getMacAddr();
            String location = Path.GetTempPath() + "\\macaddrbarcode.png";
            Console.WriteLine(location.Trim());
            GenerateBacode(macAddr, location);
            Process.Start(@"cmd.exe",@"/c " + location);
        }

        private static void GenerateBacode(string _data, string _filename)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.IncludeLabel = true;
            Image img = b.Encode(BarcodeLib.TYPE.CODE128, _data, Color.Black, Color.White, 235, 50);
            img.Save(_filename);
        }

        private static String getMacAddr()
        {
            var macAddr =
            (
                from nic in NetworkInterface.GetAllNetworkInterfaces()
                where nic.OperationalStatus == OperationalStatus.Up
                select nic.GetPhysicalAddress().ToString()
            ).FirstOrDefault();
            var finalMac = "";
            char[] macArr = macAddr.ToCharArray();
            for (int i = 0; i < macAddr.Length; i++)
            {
                if (i != 0 && i % 2 == 0)
                    finalMac += ":";
                finalMac += macArr[i];
            }

            Console.WriteLine(finalMac);
            return finalMac;
        }
    }
}