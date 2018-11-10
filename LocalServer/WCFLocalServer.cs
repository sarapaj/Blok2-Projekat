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
			throw new NotImplementedException();
		}

		public void Read(string username, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
