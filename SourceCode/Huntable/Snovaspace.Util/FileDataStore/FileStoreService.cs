using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web.UI.WebControls;
using Snovaspace.Util.Image;

namespace Snovaspace.Util.FileDataStore
{
    public class FileStoreService
    {
        private readonly IFileStore _databaseFileStore;

        public FileStoreService()
        {
            _databaseFileStore = new DatabaseFileStore();
        }

        public int? LoadFileFromFileUpload(string category, FileUpload img)
        {
            if (img.HasFile && img.PostedFile != null)
            {
                return _databaseFileStore.SaveFile(category, img.PostedFile.FileName, img.PostedFile.InputStream);
            }

            return null;
        }

        public Stream GetFileFromId(int id)
        {
            return _databaseFileStore.Read(id);
        }

        public string GetDownloadUrl(int? imageId)
        {
            if (imageId.HasValue && imageId > 0)
            {
                return "~/LoadFile.ashx?id=" + imageId;
            }
            
            return "~/HuntableImages/nomore.jpg";
        }

        public int? LoadImageFromFileUpload(string category, FileUpload fuPhoto, int height, int width)
        {
            if (fuPhoto.HasFile && fuPhoto.PostedFile != null)
            {
                string tempPath = Path.GetTempFileName();
                string resizedPath = Path.GetTempFileName();

                fuPhoto.SaveAs(tempPath);

                ImageUtilities.ResizeImage(tempPath, resizedPath, height, width, false);

                return _databaseFileStore.SaveFile(category, fuPhoto.PostedFile.FileName, File.OpenRead(resizedPath));
            }

            return null;
        }

        public int? LoadImageAndResize(string category, FileUpload fuPhoto)
        {
            if (fuPhoto.HasFile && fuPhoto.PostedFile != null)
            {
                 System.Drawing.Image myImage = System.Drawing.Image.FromStream(fuPhoto.PostedFile.InputStream);
                const decimal scaleFactor = (decimal) 0.5;
                string tempPath = Path.GetTempFileName();
                string resizedPath = Path.GetTempFileName();
                var newWidth = (int)(myImage.Width * scaleFactor);
               
                var newHeight = (int)(myImage.Height * scaleFactor);
                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(myImage, imageRectangle);
               
                fuPhoto.SaveAs(tempPath);
               
                ImageUtilities.ResizeImage(tempPath, resizedPath, newHeight, newWidth, false);

                return _databaseFileStore.SaveFile(category, fuPhoto.PostedFile.FileName, File.OpenRead(resizedPath));
            }

            return null;
        }
        public int? LoadFileFromFileUploadc(string category, FileInfo img)
        {
                Stream stream = File.OpenRead(img.FullName);
                return _databaseFileStore.SaveFile(category, img.Name, stream);
        }

        public string GetDownloadUrlDirect(int? i)
        {
            return GetDownloadUrl(i).Replace("~", "");
        }
    }
}
