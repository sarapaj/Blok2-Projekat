using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LocalServer
{

	class Program
	{
		public static List<Entity> MyEntities = new List<Entity>();
		static void Main(string[] args)
		{
			// communication with main server
			NetTcpBinding serverBinding = new NetTcpBinding();
            string serverAddress = "net.tcp://localhost:9009/MainService";
			int region1=0, region2=0;
			string r1 ="", r2="";
			do
			{
				Console.WriteLine("Unesite prvu regiju");
				r1 = Console.ReadLine();
				Console.WriteLine("Unesite drugu regiju");
				r2 = Console.ReadLine();
			}
			while (!Int32.TryParse(r1, out region1) && !Int32.TryParse(r2, out region2));

			LocalService m = new LocalService();
			Listener l = new Listener(region1, region2);
			l.Subscribe();

			using (WCFLocalServer proxy = new WCFLocalServer(serverBinding, serverAddress))
            {
				MyEntities = proxy.InitializeList(region1, region2);
            }

            // communication with client
            NetTcpBinding binding = new NetTcpBinding();
            string clientAddress = "net.tcp://localhost:9000/LocalService";
			binding.Security.Mode = SecurityMode.Transport;

			binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
			binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;

			ServiceHost host = new ServiceHost(typeof(LocalService));

            ServiceSecurityAuditBehavior newAuditBehavior = new ServiceSecurityAuditBehavior();

            newAuditBehavior.AuditLogLocation = AuditLogLocation.Application;
            newAuditBehavior.MessageAuthenticationAuditLevel = AuditLevel.Success;
            newAuditBehavior.ServiceAuthorizationAuditLevel = AuditLevel.SuccessOrFailure;
            newAuditBehavior.SuppressAuditFailure = false;

            host.Description.Behaviors.Remove<ServiceSecurityAuditBehavior>();
            host.Description.Behaviors.Add(newAuditBehavior);

            host.AddServiceEndpoint(typeof(ILocalService), binding, clientAddress);

            host.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            
            host.Open();
            Console.WriteLine("WCFService is opened. Press <enter> to finish...");
            Console.ReadLine();

            host.Close();
        }

		public class Listener
		{
			int r1 { get; set; }
			int r2 { get; set; }

			public Listener(int r1, int r2)
			{
				this.r1 = r1;
				this.r2 = r2;
			}
			public void Subscribe()
			{
				LocalService.Tick += new LocalService.TickHandler(HeardIt);
			}
			private void HeardIt(LocalService s, EventArgs e)
			{
				System.Console.WriteLine("HEARD IT");
				NetTcpBinding serverBinding = new NetTcpBinding();
				string serverAddress = "net.tcp://localhost:9009/MainService";

				using (WCFLocalServer proxy = new WCFLocalServer(serverBinding, serverAddress))
				{
					proxy.UpdateDB(MyEntities, r1,r2);
				}
			}

		}
	}
}
