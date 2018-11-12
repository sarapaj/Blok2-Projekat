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
		//List<Entity> Entities = new List<Entity>()
		//{
		//	new Entity(1,1,"Novi Sad",DateTime.Now,3434),
		//	new Entity(2,2,"Novi Sad",DateTime.Now,3489),
		//	new Entity(3,3,"Novi Sad",DateTime.Now,4645)
		//};

		public static event TickHandler Tick;
		public static EventArgs e = null;
		public delegate void TickHandler(LocalService m, EventArgs e);


        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Writer")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Reader")]
        public List<Entity> Read()
		{
			return Program.MyEntities;
		}

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Writer")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Reader")]
        public double CountAvg(int region)
		{
			List<int> temp = new List<int>();

			foreach (var item in Program.MyEntities)
			{
				if (item.Region == region)
				{
					temp.Add(item.Consumption);
				}
			}

			return temp.Average();
		}

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Writer")]
        public bool Update(int region, int month, int value, int id) // prosledjujemo redni broj meseca 1-12
		{
			foreach (var item in Program.MyEntities)
			{
				if (item.Region == region && item.Date.Month == month && item.Id==id)
				{
					item.Consumption = value;
					Tick?.Invoke(this, e);
					return true;
				}
			}

			return false;
		}

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public bool AddEntity(Entity entity)
		{
			if (!Program.MyEntities.Contains(entity))
			{
				Program.MyEntities.Add(entity);
				Tick?.Invoke(this, e);
				return true;
			}

			return false;
		}

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public bool RemoveEntity(Entity entity)
		{
			if (!Program.MyEntities.Contains(entity))
			{
				Program.MyEntities.Remove(entity);
				Tick?.Invoke(this, e);
				return true;
			}

			return false;

		}
	}
}
