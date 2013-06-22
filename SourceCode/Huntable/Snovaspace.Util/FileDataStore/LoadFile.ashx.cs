using System;
using System.Web;
using System.IO;

namespace Snovaspace.Util.FileDataStore
{
    public class LoadFile : IHttpHandler
    {
        private readonly DatabaseFileStore _databaseFileStore;

        public LoadFile()
        {
            _databaseFileStore = new DatabaseFileStore();
        }

        public void ProcessRequest(HttpContext context)
        {
            Int32 id;
            if (context.Request.QueryString["id"] != null && !string.IsNullOrWhiteSpace(context.Request.QueryString["id"]))
            {
                if (!Int32.TryParse(context.Request.QueryString["id"], out id)) return;
            }
            else
            {
                return;
            }

            var entity = _databaseFileStore.ReadEntity(id);

            if (entity != null)
            {
                Stream strm = new MemoryStream(entity.File);
                var buffer = new byte[4096];
                int byteSeq = strm.Read(buffer, 0, 4096);
                context.Response.ContentType = MimeAssistant.GetMimeType(entity.FileName);
                while (byteSeq > 0)
                {
                    context.Response.OutputStream.Write(buffer, 0, byteSeq);
                    byteSeq = strm.Read(buffer, 0, 4096);
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}