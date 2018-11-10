using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LocalServer
{
    class LocalService : ILocalService
    {
		List<Entity> Entities = new List<Entity>()
		{
			new Entity(1,1,"Novi Sad",DateTime.Now,3434),
			new Entity(2,2,"Novi Sad",DateTime.Now,3489),
			new Entity(3,3,"Novi Sad",DateTime.Now,4645)
		};

		//[PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
		//[PrincipalPermission(SecurityAction.Demand, Role = "Writer")]
		//[PrincipalPermission(SecurityAction.Demand, Role = "Reader")]
		public List<Entity> Read()
		{
			return Entities;
		}

		//[PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
		//[PrincipalPermission(SecurityAction.Demand, Role = "Writer")]
		//[PrincipalPermission(SecurityAction.Demand, Role = "Reader")]
		public double CountAvg(int region)
		{
			List<int> temp = new List<int>();

			foreach (var item in Entities)
			{
				if (item.Region == region)
				{
					temp.Add(item.Consumption);
				}
			}

			return temp.Average();
		}

		//[PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
		//[PrincipalPermission(SecurityAction.Demand, Role = "Writer")]
		public bool Update(int region, int month, int value) // prosledjujemo redni broj meseca 1-12
		{
			foreach (var item in Entities)
			{
				if (item.Region == region && item.Date.Month == month)
				{
					item.Consumption = value;
					return true;
				}
			}

			return false;
		}

	//	[PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
		public bool AddEntity(Entity entity)
		{
			if (!Entities.Contains(entity))
			{
				Entities.Add(entity);
				return true;
			}

			return false;
		}

		//[PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
		public bool RemoveEntity(Entity entity)
		{
			if (!Entities.Contains(entity))
			{
				Entities.Remove(entity);
				return true;
			}

			return false;

		}
	}
}
