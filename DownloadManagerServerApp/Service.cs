using System;
using Ziroh.Misc.Common;

namespace DownloadManagerServerApp
{
    class Service
    {
        internal void Start(Uri baseAddress)
        {
            GenericHttpService<DownloadServer, IDownloadManager> service = new GenericHttpService<DownloadServer, IDownloadManager>(baseAddress.ToString(), false);
            try
            {
                service.StartHost();
                Console.WriteLine("started");
                Console.ReadKey();
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
            }
        }
    }
}
