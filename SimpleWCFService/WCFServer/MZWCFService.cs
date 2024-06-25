using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCFServer.Requests;

namespace WCFServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class MZWCFService : IMZWCFService
    {
        public string CheckConnection(CheckConnection request)
        {
            OnRequest?.Invoke($"{DateTime.Now:HH:mm:ss}  Call to CheckConnection from {request.Name}");

            return $"You are connected to the WCF servcie ({Environment.MachineName}) as {request.Name} at {DateTime.Now:HH:mm:ss}";
        }

        public FileListResponse FileList(FileListRequest request)
        {
            var result = new FileListResponse();

            try
            {
                var filesInPath = System.IO.Directory.GetFiles(request.Path);

                var dirsInPath = System.IO.Directory.GetDirectories(request.Path);

                var list = new List<string>();
                list.AddRange(filesInPath);
                list.AddRange(dirsInPath);

                result.Path = request.Path;
                result.Files = list.ToArray();
            }
            catch (Exception)
            {
            }

            return result;
        }

        public DownloadResponse Download(DownloadRequest request)
        {
            var response = new DownloadResponse();

            if (File.Exists(request.Path))
            {
                var fileInfo = new FileInfo(request.Path);
                response.TotalLength = fileInfo.Length;

                using (FileStream fs = fileInfo.OpenRead())
                {
                    fs.Seek(request.Position, SeekOrigin.Begin);
                    var bytes = new byte[request.Length];
                    var bytesRead = fs.Read(bytes, 0, bytes.Length);
                    response.Bytes = new byte[bytesRead];
                    response.Length = bytesRead;
                    response.Path = request.Path;

                    Buffer.BlockCopy(bytes, 0, response.Bytes, 0, bytesRead);
                }
            }

            return response;
        }

        public delegate void OnRequestEventHandler(string message);
        public event OnRequestEventHandler OnRequest;
    }
}
