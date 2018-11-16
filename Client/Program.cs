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
				Console.WriteLine("Odaberite operaciju");

				if (principal.IsInRole("CountAvg"))
				{
					Console.WriteLine("1. Prikazi srednju vrednost potrosnje");
				}
				if (principal.IsInRole("Update"))
				{
					Console.WriteLine("2. Izmeni vrednost");
				}
				if (principal.IsInRole("AddEntity"))
				{
					Console.WriteLine("3. Dodaj");
				}
				if (principal.IsInRole("RemoveEntity"))
				{
					Console.WriteLine("4. Obrisi");
				}
				broj = Console.ReadLine();


				switch (broj)
				{
					case "1":
						Console.WriteLine("Unesite region:");
						string temp = Console.ReadLine();
						int region = 0;
						Int32.TryParse(temp, out region);
						double count = proxy.CountAvg(region);
						Console.WriteLine("Srednja vrednost godisnje potrosnje za region {0} je {1}", region, count);
						break;
					case "2":
                        if (principal.IsInRole("Update"))
                        {
                            Console.WriteLine("Unesite region za izmenu");
                            string r = Console.ReadLine();
                            Console.WriteLine("Unesite zeljeni mesec (1-12)");
                            string m = Console.ReadLine();
                            Console.WriteLine("Unesite zeljenu vrednost");
                            string v = Console.ReadLine();
                            Console.WriteLine("Unesite id podatka");
                            string i = Console.ReadLine();

                            int reg, month, value, id;
                            Int32.TryParse(r, out reg);
                            Int32.TryParse(m, out month);
                            Int32.TryParse(v, out value);
                            Int32.TryParse(i, out id);

                            if (proxy.Update(reg, month, value, id))
                            {
                                Console.WriteLine("Uspesna izmena");
                            }
                            else
                            {
                                Console.WriteLine("Doslo je do greske");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Niste uneli adekvatan broj");
                        }
						break;
					case "3":
                        if (principal.IsInRole("AddEntity"))
                        {
                            Console.WriteLine("Unesite id");
                            string t1 = Console.ReadLine();
                            Console.WriteLine("Unesite region");
                            string t2 = Console.ReadLine();
                            Console.WriteLine("Unesite grad");
                            string t3 = Console.ReadLine();
                            Console.WriteLine("Unesite potrosnju");
                            string t4 = Console.ReadLine();
                            int i1, i2, i4;
                            Int32.TryParse(t1, out i1);
                            Int32.TryParse(t2, out i2);
                            Int32.TryParse(t4, out i4);
                            if (proxy.AddEntity(new Entity(i1, i2, t3, DateTime.Now, i4)))
                            {
                                Console.WriteLine("Uspesno dodavanje");
                            }
                            else
                            {
                                Console.WriteLine("Doslo je do greske");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Niste uneli adekvatan broj");
                        }
                        break;
					case "4":
                        if (principal.IsInRole("RemoveEntity"))
                        {
                            Console.WriteLine("Unesite id regiona za brisanje:");
                            string removeid = Console.ReadLine();
                            int remid;
                            Int32.TryParse(removeid, out remid);
                            if (proxy.RemoveEntity(remid))
                            {
                                Console.WriteLine("Uspesno brisanje");
                            }
                            else
                            {
                                Console.WriteLine("Neuspesno brisanje");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Niste uneli adekvatan broj");
                        }
                        break;
					default:
                        Console.WriteLine("Niste uneli adekvatan broj");
                        break;
				}
			}
			Console.ReadLine();
		}
	}

}
