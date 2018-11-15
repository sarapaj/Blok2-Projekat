using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Common;
using System.Security.Principal;
using System.Threading;

namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			NetTcpBinding binding = new NetTcpBinding();
			string address = "net.tcp://localhost:9008/LocalService";

			binding.Security.Mode = SecurityMode.Transport;
			binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
			binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
			List<Entity> Entities = new List<Entity>();
			MyPrincipal principal = null;

			//WCFClient proxy = new WCFClient(binding, new EndpointAddress(new Uri(address)));

			using (WCFClient proxy = new WCFClient(binding, new EndpointAddress(new Uri(address))))
			{
		
				if (principal == null)
				{
					principal = new MyPrincipal(WindowsIdentity.GetCurrent());
				}
				string broj = "";

				if (principal.IsInRole("Read"))
				{
					//while (true)
					{

						while (broj != "1" && broj != "2")
						{
							Console.WriteLine("Odaberite operaciju");
							Console.WriteLine("1. Prikazi informacije");
							Console.WriteLine("2. Prikazi srednju vrednost potrosnje");
							broj = Console.ReadLine();
						}
					
						switch (broj)
						{
							case "1":
								Entities = proxy.Read();
								foreach (var item in Entities)
								{
									Console.WriteLine("Id:" + item.Id);
									Console.WriteLine("Region:" + item.Region);
									Console.WriteLine("Godina:" + item.Date.Year);
									Console.WriteLine("Mesec:" + item.Date.Month);
									Console.WriteLine("Potrosnja:" + item.Consumption);
									Console.WriteLine();
								}
								break;
							case "2":
								Console.WriteLine("Unesite region:");
								string temp = Console.ReadLine();
								int region = 0;
								Int32.TryParse(temp, out region);
								double count = proxy.CountAvg(region);
								Console.WriteLine("Srednja vrednost godisnje potrosnje za region {0} je {1}", region, count);
								break;
						}
					}
				}
			}
			Console.ReadLine();
		}

		static void DoSomething(WCFClient proxy)
		{
			
		}
	}

}
