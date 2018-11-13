using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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

		public List<Entity> Read()
		{
			List<Entity> temp = new List<Entity>();
			try
			{
				temp = factory.Read();
				Console.WriteLine("Read() allowed.");
               // Audit.ReadSuccess(WindowsIdentity.GetCurrent().ToString());
            }
            catch (Exception e)
			{
				Console.WriteLine("Error while trying to Read(). {0}", e.Message);
               // Audit.ReadFailed(WindowsIdentity.GetCurrent().ToString());
            }

            return temp;
		}

		public double CountAvg(int region)
		{
			double temp = 0;
			try
			{
				temp = factory.CountAvg(region);
				Console.WriteLine("CountAvg() allowed.");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while trying to CountAvg(). {0}", e.Message);
			}

			return temp;
		}

		public bool Update(int region, int month, int value, int id)
		{
			bool temp = false;
			try
			{
				temp = factory.Update(region,month, value, id);
				Console.WriteLine("Update() allowed.");
                Audit.UpdateSuccess(WindowsIdentity.GetCurrent().ToString());
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while trying to Update(). {0}", e.Message);
                Audit.UpdateFailed(WindowsIdentity.GetCurrent().ToString());
            }

            return temp;
		}

		public bool AddEntity(Entity entity)
		{
			bool temp = false;
			try
			{
				temp = factory.AddEntity(entity);
				Console.WriteLine("AddEntity() allowed.");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while trying to AddEntity(). {0}", e.Message);
			}

			return temp;
		}

		public bool RemoveEntity(Entity entity)
		{
			bool temp = false;
			try
			{
				temp = factory.RemoveEntity(entity);
				Console.WriteLine("RemoveEntity() allowed.");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while trying to RemoveEntity(). {0}", e.Message);
			}

			return temp;
		}
	}
}
