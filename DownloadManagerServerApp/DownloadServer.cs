using System;
using System.Threading.Tasks;
using UserCommonApp;
using System.ServiceModel;
using System.Net;
using System.Diagnostics;

namespace DownloadManagerServerApp
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class DownloadServer : IDownloadManager
    {
        DownloadClient downloadclient = new DownloadClient();
        public async Task DownloadBinaries(string VersionNumber)
        {
            Result updatedMSILink = null;
            try
            {
                updatedMSILink = await downloadclient.downloadMSILink(VersionNumber);
            }
            catch (Exception msg)
            {
            }
            string url = updatedMSILink.latestVersionLink.ToString();
            string fileName = "OStore.msi";
            string fileStoringLocation = @"E:\Ziroh\OStore\";
            string filePath = fileStoringLocation + fileName;
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadFileAsync(
                        new Uri(url),
                        filePath
                    );
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            CloseDesktopApplication();
            CloseSecureConnection();
            StartMSI(filePath);
            Environment.Exit(1);
        }

        private void CloseDesktopApplication()
        {
            Process[] processNames = Process.GetProcessesByName("chrome");

            foreach (Process item in processNames)
            {
                item.Kill();
            }
        }

        private void CloseSecureConnection()
        {
            throw new NotImplementedException();
        }

        private void StartMSI(string filePath)
        {
            Process installerProcess = new Process();
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.Arguments = filePath +"  /q";
            processInfo.FileName = "msiexec";
            installerProcess.StartInfo = processInfo;
            installerProcess.Start();
            installerProcess.WaitForExit();
        }
    }
}
