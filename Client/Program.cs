using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Common;

namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			NetTcpBinding binding = new NetTcpBinding();
			string address = "net.tcp://localhost:9000/LocalService";

			binding.Security.Mode = SecurityMode.Transport;
			binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
			binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;

			List<Entity> Entities = new List<Entity>();

			using (WCFClient proxy = new WCFClient(binding, new EndpointAddress(new Uri(address))))
			{
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

				if (proxy.Update(1, 11, 345))
				{
					Console.WriteLine("Uspesna izmena");
				}
				else
				{
					Console.WriteLine("Neuspesna izmena");
				}

				Console.WriteLine("Odaberite id za brisanje");
				string temp = Console.ReadLine();
				int id=0;
				Int32.TryParse(temp, out id);

				if (proxy.RemoveEntity(Entities.Find(x => x.Id == id)))
				{
					Console.WriteLine("Uspesno brisanje");
				}
				else
				{
					Console.WriteLine("Neuspesno brisanje");
				}

			}

			Console.ReadLine();
		}
	}

}
