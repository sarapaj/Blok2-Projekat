using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace LocalServer
{
	class Program
	{
		static void Main(string[] args)
		{
            // communication with main server
            NetTcpBinding serverBinding = new NetTcpBinding();
            string serverAddress = "net.tcp://localhost:9001/MainService";

            using (WCFLocalServer proxy = new WCFLocalServer(serverBinding, serverAddress))
            {
                //proxy.Read("Visnja", "Visnja.txt");
            }



            // communication with client
            NetTcpBinding clientBinding = new NetTcpBinding();
            string clientAddress = "net.tcp://localhost:9000/LocalService";

            ServiceHost host = new ServiceHost(typeof(LocalService));
            host.AddServiceEndpoint(typeof(ILocalService), clientBinding, clientAddress);
            //host.Authorization.ServiceAuthorizationManager = new MyAuthorizationManager();

            host.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });


            host.Open();
            Console.WriteLine("WCFService is opened. Press <enter> to finish...");
            Console.ReadLine();

            host.Close();
        }
	}
}
