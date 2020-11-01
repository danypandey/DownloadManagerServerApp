using System;
using System.Threading.Tasks;
using UserCommonApp;
using System.ServiceModel;
using System.Net;

namespace DownloadManagerServerApp
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class DownloadServer : IDownloadManager
    {
        //DownloadClient downloadclient = new DownloadClient();
        public async Task DownloadBinaries(string VersionNumber)
        {
            string updatedMSILink = null;
            /*Result validateVersion = null;
            try
            {
                validateVersion = await downloadclient.downloadMSILink(VersionNumber);
            }
            catch (Exception msg)
            {
            }*/
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFileAsync(
                    // Param1 = Link of file
                    new System.Uri(updatedMSILink),
                    // Param2 = Path to save
                    "Path Where File Will Be Saved"
                );
            }
            CloseDesktopApplication();
            CloseSecureConnection();
            await StartMSI();
        }

        private void CloseDesktopApplication()
        {
            throw new NotImplementedException();
        }

        private void CloseSecureConnection()
        {
            throw new NotImplementedException();
        }

        private Task StartMSI()
        {
            throw new NotImplementedException();
        }
    }
}
