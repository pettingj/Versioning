using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Version
{
    internal class Program
    {
        // Methods
        private static string GetVersion(FileInfo file)
        {
            System.Version version = Assembly.LoadFrom(file.FullName).GetName().Version;
            return string.Format("{0}.{1}.{2}.{3}", new object[] { version.Major, version.Minor.ToString("D2"), version.Build.ToString("D3"), version.Revision.ToString("D2") });
        }

        private static int Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.Out.WriteLine("Versioning [Msi Package] [Dll] [New Filename]");
                return 0;
            }
            FileInfo info = new FileInfo(args[0]);
            FileInfo file = new FileInfo(args[1]);
            string format = args[2];
            if (!info.Exists)
            {
                Console.Out.WriteLine("Unable to find msi package: " + args[0]);
            }
            if (!file.Exists)
            {
                Console.Out.WriteLine("Unable to find dll" + args[1]);
            }
            if (!info.Exists || !file.Exists)
            {
                return 8;
            }
            string version = GetVersion(file);
            string destFileName = string.Format(format, version);
            info.MoveTo(destFileName);
            return 0;
        }
    }
}
