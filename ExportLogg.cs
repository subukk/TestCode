using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Crm.Sdk.Messages;

namespace DuplicateDelRulePub
{
    public class ExportLogg :IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {

            throw new InvalidPluginExecutionException(" Error The Context: ");
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));           
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);          
            ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            tracing.Trace("Tracing implemented successfully!");
            if (context != null &&
                (context.MessageName == "ExportToExcel" ||
                 context.MessageName == "ExportDynamicToExcel"))
            {
                throw new InvalidPluginExecutionException(" Error The Context: " + context.MessageName );
            }

            
        }
    }
}
