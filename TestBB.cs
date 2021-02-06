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
    public class TestBB : IPlugin
    {
    //    public void Execute(IServiceProvider serviceProvider)
    //    {

    //        throw new InvalidPluginExecutionException(" Error The Context: ");
    //        IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
    //        IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
    //        IOrganizationService service = factory.CreateOrganizationService(context.UserId);
    //        ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
    //        tracing.Trace("Tracing implemented successfully!");
    //        if (context != null &&
    //            (context.MessageName == "ExportToExcel" ||
    //             context.MessageName == "ExportDynamicToExcel"))
    //        {
    //            throw new InvalidPluginExecutionException(" Error The Context: " + context.MessageName);
    //        }

    //    }

                public void Execute(IServiceProvider serviceProvider)
        {
            //<snippetAdvancedPlugin2>
            //Extract the tracing service for use in debugging sandboxed plug-ins.
            ITracingService tracingService =
                (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Obtain the execution context from the service provider.
            IPluginExecutionContext context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));

            // For this sample, execute the plug-in code only while the client is online. 
            tracingService.Trace("AdvancedPlugin: Verifying the client is not offline.");
            if (context.IsExecutingOffline || context.IsOfflinePlayback)
                return;

            // The InputParameters collection contains all the data passed 
            // in the message request.
            if (context.InputParameters.Contains("Target") &&
                context.InputParameters["Target"] is Entity)
            {
                // Obtain the target entity from the Input Parameters.
                tracingService.Trace
                    ("AdvancedPlugin: Getting the target entity from Input Parameters.");
                Entity entity = (Entity)context.InputParameters["Target"];

                // Obtain the image entity from the Pre Entity Images.
                tracingService.Trace
                    ("AdvancedPlugin: Getting image entity from PreEntityImages.");
                //<snippetAdvancedPlugin1>
                Entity image = (Entity)context.PreEntityImages["Target"];
                //</snippetAdvancedPlugin1>
                //</snippetAdvancedPlugin2>

                // Verify that the target entity represents a contact.
                // If not, this plug-in was not registered correctly.
                tracingService.Trace
                    ("AdvancedPlugin: Verifying that the target entity represents a contact.");
                if (entity.LogicalName != "contact")
                    return;

                //tracingService.Trace("AdvancedPlugin: Creating the description.");
                //String descriptionMessage = "Old full name: " +
                //    image["fullname"] + " -- Unsecured: " + _unsecureString +
                //    ", Secured: " + _secureString;

                //tracingService.Trace
                //    ("AdvancedPlugin: Checking if the target entity doesn't have a description attribute.");
                if (entity.Attributes.Contains("description") == false)
                {
                    tracingService.Trace
                        ("AdvancedPlugin: Adding a description attribute with the new value.");
                    //entity.Attributes.Add("description", descriptionMessage);
                }
            }
        }
    }
}


