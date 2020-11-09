using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace DownloadManagerServerApp
{
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
            Environment.Exit(0);
        }
    }
}
