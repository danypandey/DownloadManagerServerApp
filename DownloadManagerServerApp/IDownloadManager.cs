using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace DownloadManagerServerApp
{
    [ServiceContract]
    interface IDownloadManager
    {
        /*
         * Get 
         */
        [OperationContract]
        [WebGet(UriTemplate = "/updateservice/download/{UpgradeReferenceId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task DownloadBinaries(string UpgradeReferenceId);
    }
}
