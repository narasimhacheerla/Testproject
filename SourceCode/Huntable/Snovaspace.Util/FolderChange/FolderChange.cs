using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;

namespace Snovaspace.Util.FolderChange
{
    public class FolderChange
    {
        public static void Change()
        {
            const string folderBase = @"C:\TT\_SourceCode\Tmss.Trapper";
            const string copyToPath = @"C:\TT\_SourceCode\Copyto";

            CopyModified(folderBase, folderBase, copyToPath);

            Console.WriteLine("Completed");
            Console.ReadLine();
        }

        private static void CopyModified(string folderBase, string originalBase, string copyToPath)
        {
            Console.Write(".");

            Parallel.ForEach(Directory.GetDirectories(folderBase), x => CopyModified(x, originalBase, copyToPath));

            Parallel.ForEach(GetFiles(folderBase, "*.cs|*.csproj", SearchOption.TopDirectoryOnly), file =>
            {
                DateTime time = new FileInfo(file).LastWriteTime;

                if (DateTime.Now.Subtract(time).TotalHours < 5)
                {
                    var directoryInfo = new DirectoryInfo(file).Parent;
                    if (directoryInfo != null)
                    {
                        string directory = directoryInfo.FullName;

                        string newDirectory = directory.Replace(originalBase, copyToPath);

                        Directory.CreateDirectory(newDirectory);

                        File.Copy(file, Path.Combine(newDirectory, Path.GetFileName(file)), true);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Copied " + file);
                }
            });
        }

        public static string[] GetFiles(string sourceFolder, string filter, SearchOption searchOption)
        {
            // ArrayList will hold all file names
            var alFiles = new ArrayList();

            // Create an array of filter string
            string[] multipleFilters = filter.Split('|');

            // for each filter find mathing file names
            foreach (string fileFilter in multipleFilters)
            {
                // add found file names to array list
                alFiles.AddRange(Directory.GetFiles(sourceFolder, fileFilter, searchOption));
            }

            // returns string array of relevant file names
            return (string[])alFiles.ToArray(typeof(string));
        }
    }
}