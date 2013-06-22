using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Snovaspace.Util.FileDataStore
{
    public class DatabaseFileStore : IFileStore
    {
        private readonly string _projectName;

        public DatabaseFileStore(string projectName)
        {
            _projectName = projectName;
        }

        public DatabaseFileStore()
        {
            _projectName = ConfigurationManager.AppSettings["project"];
        }

        public Stream Read(int id)
        {
            using (var dataEntities = new FileStoreEntities())
            {
                UserFileContent file = dataEntities.UserFileContents.FirstOrDefault(x => x.Id == id);

                if (file != null) return new MemoryStream(file.File);
            }

            return null;
        }

        public UserFileContent ReadEntity(int id)
        {
            using (var dataEntities = new FileStoreEntities())
            {
                 return dataEntities.UserFileContents.FirstOrDefault(x => x.Id == id);
            }
        }

        public Stream Read(string path, string fileName)
        {
            return Read(_projectName, path, fileName);
        }

        public Stream Read(string projectName, string path, string fileName)
        {
            using (var dataEntities = new FileStoreEntities())
            {
                UserFileContent file = dataEntities.UserFileContents.FirstOrDefault(x => (x.Path.ToLower() == path.ToLower() && fileName.ToLower() == x.FileName.ToLower() && projectName.ToLower() == _projectName.ToLower()));

                if (file != null) return new MemoryStream(file.File);
            }

            return null;
        }

        public int? SaveFile(string path, string fileName, Stream stream)
        {
            using (var dataEntities = new FileStoreEntities())
            {
                var file = new UserFileContent();

                var fileBytes = new Byte[stream.Length];

                stream.Read(fileBytes, 0, (int)stream.Length);

                file.ProjectName = _projectName;
                file.Path = path;
                file.FileName = fileName;
                file.File = fileBytes;
                file.CreatedDateTime = DateTime.Now;

                dataEntities.AddToUserFileContents(file);

                dataEntities.SaveChanges();

                return file.Id;
            }
        }
    }
}