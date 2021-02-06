using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Crm.Sdk.Messages;
using System.Net;
using System.ServiceModel.Web;

namespace DuplicateDelRulePub
{
    public class callServiceApp : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {

            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                try
                {

                    // using WebClient
                    WebClient webClient1 = new WebClient();
                    string data1 = webClient1.DownloadString("www.kksubhash.com/service1.svc/getdata");

                    // using WebChannelFactory
                    WebChannelFactory<IService1> factory = new WebChannelFactory<IService1>(new Uri("www.kksubhash.com/service1.svc"));
                    IService1 proxy = factory.CreateChannel();
                    string returnResult = proxy.GetData(2222);
                    throw new InvalidPluginExecutionException("Service returned  the data:" + returnResult.ToString());
                }
                catch (Exception ex)
                {
                    throw new InvalidPluginExecutionException(ex.ToString());
                }
            }
            //IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));         
            //IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            //IOrganizationService service = factory.CreateOrganizationService(context.UserId);            
            //ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            //tracing.Trace("Tracing implemented successfully!");
            //if (context.MessageName == "PublishAll")
            //{
            //    PublishRules(service, tracing);
            //}
        }

    }
}
