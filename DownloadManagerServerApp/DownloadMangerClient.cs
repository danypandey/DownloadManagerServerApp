using System;
using System.Threading.Tasks;
using Ziroh.Misc.Common;

namespace DownloadManagerServerApp
{
    class DownloadMangerClient
    {
        string baseUri = default(string);
        GenericRestClient client;
        public DownloadMangerClient()
        {
            baseUri = "http://127.0.0.1:8080";
            client = new GenericRestClient(baseUri);
        }


        internal async Task<byte[]> GetMSI(string clientConfiguration)
        {
            string relativeUrl = string.Format("/updateservice/download/{0}", clientConfiguration);
            byte[] updatedMSI = null;
            Action<byte[]> onSuccess = new Action<byte[]>((validateResult =>
            {
                updatedMSI = validateResult;
            }));
            Action<HttpFailure> onFailure = new Action<HttpFailure>((failure) =>
            {
                Console.WriteLine("http failure " + failure.Message);
            });
            await client.GetAsync(onSuccess, onFailure, relativeUrl);

            return updatedMSI;
        }
    }
}
