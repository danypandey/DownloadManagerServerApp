using System;
using System.Threading.Tasks;
using UserCommonApp;
using Ziroh.Misc.Common;

namespace DownloadManagerServerApp
{
    class DownloadClient
    {
        string baseUri = default(string);
        GenericRestClient client;
        public DownloadClient()
        {
            baseUri = "http://127.0.0.1:8080";
            client = new GenericRestClient(baseUri);
        }

        public async Task<UserCommonApp.Result> downloadMSILink(string latestAppVersion)
        {
            UserCommonApp.Result clientUpdateResult = await DownloadMSILink(latestAppVersion);
            Console.WriteLine(clientUpdateResult.latestVersionLink);
            return clientUpdateResult;
        }

        private async Task<UserCommonApp.Result> DownloadMSILink(string LatestAppVersion)
        {
            string relativeUrl = string.Format("/updateservice/download/{0}", LatestAppVersion);
            UserCommonApp.Result updatedMSILink = null;
            Action<UserCommonApp.Result> onSuccess = new Action<UserCommonApp.Result>((validateResult =>
            {
                updatedMSILink = validateResult;
            }));
            Action<HttpFailure> onFailure = new Action<HttpFailure>((failure) =>
            {
                Console.WriteLine("http failure " + failure.Message);
            });
            await client.GetAsync(onSuccess, onFailure, relativeUrl);

            return updatedMSILink;
        }
    }
}
