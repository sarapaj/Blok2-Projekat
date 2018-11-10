using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LocalServer
{ 
    public class WCFLocalServer : ChannelFactory<IMainService>, IMainService, IDisposable
    {
        IMainService factory;

        public WCFLocalServer(NetTcpBinding binding, string address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

		public List<Entity> InitializeList(int region1, int region2)
		{
			List<Entity> temp = new List<Entity>();

			try
			{
				temp = factory.InitializeList(region1, region2);
				Console.WriteLine("Initialize() allowed.");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while trying to RemoveEntity(). {0}", e.Message);
			}

			return temp;
		}

		public void UpdateDB(List<Entity> lista, int region1, int region2)
		{
			try
			{
				factory.UpdateDB(lista, region1, region2);
				Console.WriteLine("Initialize() allowed.");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while trying to RemoveEntity(). {0}", e.Message);
			}
		}

		public void Read(string username, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
