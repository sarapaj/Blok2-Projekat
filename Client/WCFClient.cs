﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
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
                Audit.ReadSuccess(WindowsIdentity.GetCurrent());
            }
            catch (Exception e)
			{
				Console.WriteLine("Error while trying to Read(). {0}", e.Message);
                Audit.ReadFailed(WindowsIdentity.GetCurrent());
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
                Audit.CountSuccess(WindowsIdentity.GetCurrent());
            }
            catch (Exception e)
			{
				Console.WriteLine("Error while trying to CountAvg(). {0}", e.Message);
                Audit.CountFailed(WindowsIdentity.GetCurrent());
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
                Audit.UpdateSuccess(WindowsIdentity.GetCurrent());
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while trying to Update(). {0}", e.Message);
                Audit.UpdateFailed(WindowsIdentity.GetCurrent());
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
                Audit.AddSuccess(WindowsIdentity.GetCurrent());
            }
            catch (Exception e)
			{
				Console.WriteLine("Error while trying to AddEntity(). {0}", e.Message);
                Audit.AddFailed(WindowsIdentity.GetCurrent());
            }

            return temp;
		}

		public bool RemoveEntity(int id)
		{
			bool temp = false;
			try
			{
				temp = factory.RemoveEntity(id);
				Console.WriteLine("RemoveEntity() allowed.");
                Audit.RemoveSuccess(WindowsIdentity.GetCurrent());
            }
            catch (Exception e)
			{
				Console.WriteLine("Error while trying to RemoveEntity(). {0}", e.Message);
                Audit.RemoveFailed(WindowsIdentity.GetCurrent());
            }

            return temp;
		}
	}
}
