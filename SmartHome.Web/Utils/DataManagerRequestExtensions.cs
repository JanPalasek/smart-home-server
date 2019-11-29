using SmartHome.DomainCore.Data;
using Syncfusion.EJ2.Base;

namespace SmartHome.Web.Utils
{
    public static class DataManagerRequestExtensions
    {
        public static PagingArgs ToPagingArgs(this DataManagerRequest dataManagerRequest)
        {
            return new PagingArgs(dataManagerRequest.Skip, dataManagerRequest.Take);
        }
    }
}