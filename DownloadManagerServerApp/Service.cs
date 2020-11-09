using System;
using Ziroh.Misc.Common;

namespace DownloadManagerServerApp
{
    class Service
    {
        internal void Start(Uri baseAddress)
        {
            GenericHttpService<DownloadManagerServer, IDownloadManager> service = new GenericHttpService<DownloadManagerServer, IDownloadManager>(baseAddress.ToString(), false);
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
