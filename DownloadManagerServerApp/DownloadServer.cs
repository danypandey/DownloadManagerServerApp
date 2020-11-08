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
            string fileName = System.IO.Path.GetFileName(url);
            string fileStoringLocation = @"E:\Ziroh\OStore\";
            string filePath = fileStoringLocation + fileName;
            if (!String.IsNullOrEmpty(url))
            {
                try
                {
                    using (WebClient wc = new WebClient())
                    {
                        //wc.Credentials = new NetworkCredential("", "");

                        wc.DownloadProgressChanged += (o, e) =>
                        {
                            Console.WriteLine($"Download Status: {e.ProgressPercentage}%. ");
                        };

                        wc.DownloadFileCompleted += (o, e) =>
                        {
                            Console.WriteLine("Download Completed");
                        };

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
            }
            CloseDesktopApplication();
            CloseSecureConnection();
            StartMSI(filePath);
            Environment.Exit(1);
        }

        private void CloseDesktopApplication()
        {
            Process[] processNames = Process.GetProcessesByName("ostore");

            try
            {
                foreach (Process item in processNames)
                {
                    item.Kill();
                    item.WaitForExit();
                }
            }
            catch (Exception msg)
            {

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
            processInfo.Arguments = filePath + "  /q";
            processInfo.FileName = "msiexec";
            installerProcess.StartInfo = processInfo;
            installerProcess.Start();
            installerProcess.WaitForExit();
        }
    }
}
