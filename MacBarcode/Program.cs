using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;

namespace MacBarcode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            String macAddr = getMacAddr();
            String location = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\test.png";
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
                {
                    finalMac += ":";
                }

                finalMac += macArr[i];
            }

            Console.WriteLine(finalMac);
            return finalMac;
        }

        [DllImport("shell32.dll")]
        static extern int FindExecutable(string lpFile, string lpDirectory, [Out] StringBuilder lpResult);

        public static void OpenImage(string imagePath)
        {
            var exePathReturnValue = new StringBuilder();
            FindExecutable(Path.GetFileName(imagePath), Path.GetDirectoryName(imagePath), exePathReturnValue);
            var exePath = exePathReturnValue.ToString();
            Console.WriteLine("exepath: " + exePath);
            var arguments = "\"" + imagePath + "\"";

            // Handle cases where the default application is photoviewer.dll.
            if (Path.GetFileName(exePath).Equals("photoviewer.dll", StringComparison.InvariantCultureIgnoreCase))
            {
                arguments = "\"" + exePath + "\", ImageView_Fullscreen " + imagePath;
                exePath = "rundll32";
            }
            var process = new Process();
            process.StartInfo.FileName = exePath;
            process.StartInfo.Arguments = arguments;
            Console.WriteLine("args: " + arguments);

            process.Start();
        }
    }
}
