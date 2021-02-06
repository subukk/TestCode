using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateDelRulePub
{
    public class basicServiceCall : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            try
            {
                //Using Service refrence
                //BasicHttpBinding myBinding = new BasicHttpBinding();
                //myBinding.Name = "BasicHttpBinding_IService1";
                //myBinding.Security.Mode = BasicHttpSecurityMode.None;
                //myBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                //myBinding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
                //myBinding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
                //EndpointAddress endPointAddress = new EndpointAddress("http://localhost:58844/Service1.svc");
                //ServiceReference1.Service1Client myClient = new ServiceReference1.Service1Client(myBinding, endPointAddress);
                //MessageBox.Show(myClient.GetData());


                BasicHttpBinding myBinding = new BasicHttpBinding();
                myBinding.Name = "BasicHttpBinding_IPOCService";
                myBinding.Security.Mode = BasicHttpSecurityMode.None;
                myBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                myBinding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
                myBinding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;

                EndpointAddress endPointAddress = new EndpointAddress("http://www.kksubhash.com/service1.svc");
                ChannelFactory<IService1> factory = new ChannelFactory<IService1>(myBinding, endPointAddress);
                IService1 channel = factory.CreateChannel();
                string returnResult = channel.GetData(2222);
                factory.Close();
                throw new InvalidPluginExecutionException("Service returned  the data:" + returnResult);
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(ex.ToString());
            }
        }
    }
}
