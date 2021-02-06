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
    public class DuplicateRulePub : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            //Obtain the execution context from the service provider.
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            //Get a reference to the Organization service.
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            //Extract the tracing service for use in debugging sandboxed plug-ins
            ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            tracing.Trace("Tracing implemented successfully!");
            if (context.MessageName == "PublishAll")
            {
                PublishRules(service, tracing);
            }
        }

        private void PublishRules(IOrganizationService service, ITracingService tracing)
        {
            EntityCollection rules = GetDuplicateDetectionRules(service);
            tracing.Trace("Obtained " + rules.TotalRecordCount.ToString() + " duplicate detection rules.");
            if (rules.TotalRecordCount >= 1)
            {
                // Create an ExecuteMultipleRequest object.
                ExecuteMultipleRequest request = new ExecuteMultipleRequest()
                {
                    // Assign settings that define execution behavior: don't continue on error, don't return responses. 
                    Settings = new ExecuteMultipleSettings()
                    {
                        ContinueOnError = false,
                        ReturnResponses = false
                    },
                    // Create an empty organization request collection.
                    Requests = new OrganizationRequestCollection()
                };
                //Create a collection of PublishDuplicateRuleRequests, and execute them in one batch
                foreach (Entity entity in rules.Entities)
                {
                    PublishDuplicateRuleRequest publishReq = new PublishDuplicateRuleRequest { DuplicateRuleId = entity.Id };
                    request.Requests.Add(publishReq);
                }
                service.Execute(request);
            }
            else
            {
                tracing.Trace("Plugin execution cancelled, as there are no duplicate detection rules to publish.");
                return;
            }
        }

        private EntityCollection GetDuplicateDetectionRules(IOrganizationService service)
        {
            QueryExpression qe = new QueryExpression("duplicaterule");
            qe.ColumnSet = new ColumnSet("duplicateruleid");
            ConditionExpression condition1 = new ConditionExpression();
            condition1.AttributeName = "statecode";
            condition1.Operator = ConditionOperator.Equal;
            condition1.Values.Add(0);

            ConditionExpression condition2 = new ConditionExpression();
            condition2.AttributeName = "statuscode";
            condition2.Operator = ConditionOperator.Equal;
            condition2.Values.Add(0);

            FilterExpression filter = new FilterExpression();
            filter.FilterOperator = LogicalOperator.And;
            filter.Conditions.Add(condition1);
            filter.Conditions.Add(condition2);
            qe.Criteria.AddFilter(filter);
            //Have to add this, otherwise the record count won't be returned correctly
            qe.PageInfo.ReturnTotalRecordCount = true;
            return service.RetrieveMultiple(qe);
        }

    }
}
