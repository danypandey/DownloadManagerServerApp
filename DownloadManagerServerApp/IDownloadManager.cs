using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using UserCommonApp;

namespace DownloadManagerServerApp
{
    [ServiceContract]
    interface IDownloadManager
    {
        /*
         * Get 
         */
        [OperationContract]
        [WebGet(UriTemplate = "/updateservice/download/{VersionNumber}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task DownloadBinaries(string VersionNumber);
    }
}
