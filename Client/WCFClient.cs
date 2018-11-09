using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class WCFClient : ChannelFactory<ILocalService>, ILocalService, IDisposable
    {
        ILocalService factory;

        public WCFClient(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public void Read()
        {
            try
            {
                factory.Read();
                Console.WriteLine("Read() allowed.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to Read(). {0}", e.Message);
            }
        }

        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }

            this.Close();
        }

		public void Count()
		{
			try
			{
				factory.Count();
				Console.WriteLine("Count() allowed.");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while trying to Count(). {0}", e.Message);
			}
		}

		public void Update()
		{
			try
			{
				factory.Update();
				Console.WriteLine("Update() allowed.");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while trying to Update(). {0}", e.Message);
			}
		}

		public void AddEntity()
		{
			try
			{
				factory.AddEntity();
				Console.WriteLine("AddEntity() allowed.");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while trying to AddEntity(). {0}", e.Message);
			}
		}

		public void RemoveEntity()
		{
			try
			{
				factory.RemoveEntity();
				Console.WriteLine("RemoveEntity() allowed.");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while trying to RemoveEntity(). {0}", e.Message);
			}
		}
	}
}
