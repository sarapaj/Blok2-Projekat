using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	class Program
	{
		static void Main(string[] args)
		{
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9001/MainService";

            ServiceHost host = new ServiceHost(typeof(MainService));
            host.AddServiceEndpoint(typeof(IMainService), binding, address);

            host.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });

            host.Open();

            Console.WriteLine("SecurityService service is started.");
            Console.WriteLine("Press <enter> to stop service...");
			DataIO data = new DataIO();

            //Serijalizacija liste
            List<Entity> lista = new List<Entity>(){
            new Entity(1,1,"Novi Sad",DateTime.Now,3434),
            new Entity(2,2,"Novi Sad",DateTime.Now,3489),
            new Entity(3,3,"Novi Sad",DateTime.Now,4645)
            };
            data.SerializeObject<List<Entity>>(lista, "db.xml");

            Console.ReadLine();
            host.Close();
        }
	}
}
