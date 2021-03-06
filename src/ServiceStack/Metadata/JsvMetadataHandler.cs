using System;
using System.Web.UI;
using ServiceStack.Host;
using ServiceStack.Text;
using ServiceStack.Web;

namespace ServiceStack.Metadata
{
	public class JsvMetadataHandler : BaseMetadataHandler
    {
        public override Format Format { get { return Format.Jsv; } }
		
		protected override string CreateMessage(Type dtoType)
        {
            var requestObj = AutoMappingUtils.PopulateWith(Activator.CreateInstance(dtoType));
			return requestObj.SerializeAndFormat();
        }

        protected override void RenderOperations(HtmlTextWriter writer, IHttpRequest httpReq, ServiceMetadata metadata)
        {
            var defaultPage = new OperationsControl
            {
				Title = HostContext.ServiceName,
                OperationNames = metadata.GetOperationNamesForMetadata(httpReq, Format),
                MetadataOperationPageBodyHtml = HostContext.Config.MetadataOperationPageBodyHtml,
            };

            defaultPage.RenderControl(writer);
        }

    }
}