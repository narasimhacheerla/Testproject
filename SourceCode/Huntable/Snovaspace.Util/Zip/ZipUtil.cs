using System;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
using Snovaspace.Util.Logging;

namespace Snovaspace.Util.Zip
{
    public class ZipUtil
    {
        public static void TarUnzip(string original, string workingDirectory)
        {
            LoggingManager.Info("Unzipping " + original + " into " + workingDirectory);
            var dataBuffer = new byte[4096];
            string newFileName = null;

            using (Stream s = new GZipInputStream(File.OpenRead(original)))
            {
                var directoryInfo = new DirectoryInfo(original).Parent;
                if (directoryInfo != null)
                    newFileName = Path.Combine(directoryInfo.FullName, Path.GetFileNameWithoutExtension(original));

                using (FileStream fs = File.Create(newFileName))
                {
                    StreamUtils.Copy(s, fs, dataBuffer);
                }
            }

            var tarApp = new Tar();
            var parent = new DirectoryInfo(original).Parent;
            if (parent != null)
                tarApp.InstanceMain(new List<string> { "-xvf", newFileName }.ToArray(), parent.FullName);
        }

        public static void Unzip(string original, string unzippedDirectory)
        {
            using (var s = new ZipInputStream(File.OpenRead(original)))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    Console.WriteLine(theEntry.Name);

                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);

                    if (directoryName != null) directoryName = Path.Combine(unzippedDirectory, directoryName);

                    // create directory
                    if (!string.IsNullOrEmpty(directoryName)) Directory.CreateDirectory(directoryName);

                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(theEntry.Name))
                        {
                            var data = new byte[2048];
                            while (true)
                            {
                                int size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}