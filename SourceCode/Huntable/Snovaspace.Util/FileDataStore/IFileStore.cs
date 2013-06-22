using System.Drawing.Imaging;
using System.IO;

namespace Snovaspace.Util.FileDataStore
{
    public interface IFileStore
    {
        int? SaveFile(string path, string fileName, Stream stream);
        Stream Read(int id);
        UserFileContent ReadEntity(int id);
        Stream Read(string path, string fileName);
        Stream Read(string projectName, string path, string fileName);
       
    }
}