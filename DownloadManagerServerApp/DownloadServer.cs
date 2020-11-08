using System;
using System.Threading.Tasks;
using UserCommonApp;
using System.ServiceModel;
using System.Net;
using System.Diagnostics;
using System.IO;

namespace DownloadManagerServerApp
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class DownloadServer : IDownloadManager
    {
        DownloadClient downloadclient = new DownloadClient();
        public async Task DownloadBinaries(string VersionNumber)
        {
            byte[] updatedMSI = null;

            try
            {
                updatedMSI = await downloadclient.downloadMSI(VersionNumber);
            }
            catch (Exception msg)
            {
            }

            /*string fileName = System.IO.Path.GetFileName(url);
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
            }*/

            string filePath = @"E:\Ziroh\OStore\setup.msi";
            string msiFilePath = @"/i E:\Ziroh\OStore\setup.msi";
            File.WriteAllBytes(filePath, updatedMSI);

            CloseDesktopApplication();
            CloseSecureConnection();
            StartMSI(msiFilePath);
            Environment.Exit(1);
        }

        private void CloseDesktopApplication()
        {
            Process[] processNames = Process.GetProcessesByName("mspaint");

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
            Process[] processNames = Process.GetProcessesByName("notepad");

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

        private void StartMSI(string msiPath)
        {
            Process installerProcess = new Process();
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.Arguments = msiPath;
            processInfo.FileName = "msiexec";
            installerProcess.StartInfo = processInfo;
            installerProcess.Start();
            Environment.Exit(1);

            /*Process installerProcess = new Process();
            installerProcess.StartInfo.FileName = Path.GetFileName(filePath);
            installerProcess.StartInfo.Verb = "runas";
            installerProcess.StartInfo.WorkingDirectory = filePath;
            installerProcess.Start();*/
        }
    }
}
