﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
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
		MyPrincipal principal = null;


        public List<Entity> Read()
		{
			if (principal == null)
			{
				principal = new MyPrincipal((WindowsIdentity)Thread.CurrentPrincipal.Identity);
			}

			if (principal.IsInRole("Read"))
			{
				return Program.MyEntities;
			}
			else
			{
				Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + "() unsuccessfully executed by " + Thread.CurrentPrincipal.Identity.Name.Split('\\')[1]);
				throw new SecurityException(System.Reflection.MethodBase.GetCurrentMethod().Name + "() unsuccessfully executed by " + Thread.CurrentPrincipal.Identity.Name.Split('\\')[1]);
			}
		}
		

        
        public double CountAvg(int region)
		{
			if (principal == null)
			{
				principal = new MyPrincipal((WindowsIdentity)Thread.CurrentPrincipal.Identity);
			}

			if (principal.IsInRole("CountAvg"))
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
			else
			{
				Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + "() unsuccessfully executed by " + Thread.CurrentPrincipal.Identity.Name.Split('\\')[1]);
				throw new SecurityException(System.Reflection.MethodBase.GetCurrentMethod().Name + "() unsuccessfully executed by " + Thread.CurrentPrincipal.Identity.Name.Split('\\')[1]);
			}
		}

    
        public bool Update(int region, int month, int value, int id) // prosledjujemo redni broj meseca 1-12
		{
			if (principal == null)
			{
				principal = new MyPrincipal((WindowsIdentity)Thread.CurrentPrincipal.Identity);
			}

			if (principal.IsInRole("Update"))
			{
				foreach (var item in Program.MyEntities)
				{
					if (item.Region == region && item.Date.Month == month && item.Id == id)
					{
						item.Consumption = value;
						Tick?.Invoke(this, e);
						return true;
					}
				}

				return false;
			}
			else
			{
				Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + "() unsuccessfully executed by " + Thread.CurrentPrincipal.Identity.Name.Split('\\')[1]);
				throw new SecurityException(System.Reflection.MethodBase.GetCurrentMethod().Name + "() unsuccessfully executed by " + Thread.CurrentPrincipal.Identity.Name.Split('\\')[1]);
			}
		}

        public bool AddEntity(Entity entity)
		{

			if (principal == null)
			{
				principal = new MyPrincipal((WindowsIdentity)Thread.CurrentPrincipal.Identity);
			}

			if (principal.IsInRole("AddEntity"))
			{
				if (!Program.MyEntities.Contains(entity) && Program.MyEntities.Find(x=> x.Id == entity.Id) == null)
				{
					Program.MyEntities.Add(entity);
					Tick?.Invoke(this, e);
					return true;
				}

				return false;
			}
			else
			{
				Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + "() unsuccessfully executed by " + Thread.CurrentPrincipal.Identity.Name.Split('\\')[1]);
				throw new SecurityException(System.Reflection.MethodBase.GetCurrentMethod().Name + "() unsuccessfully executed by " + Thread.CurrentPrincipal.Identity.Name.Split('\\')[1]);
			}
		}

        public bool RemoveEntity(int id)
		{
			if (principal == null)
			{
				principal = new MyPrincipal((WindowsIdentity)Thread.CurrentPrincipal.Identity);
			}

			Entity temp = null;
			if (principal.IsInRole("RemoveEntity"))
			{
				foreach (var ent in Program.MyEntities)
				{
					if (ent.Id == id)
						temp = ent;
				}

				if (temp != null)
				{
					Program.MyEntities.Remove(temp);
					Tick?.Invoke(this, e);
					return true;
				}

				return false;
			}
			else
			{
				Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + "() unsuccessfully executed by " + Thread.CurrentPrincipal.Identity.Name.Split('\\')[1]);
				throw new SecurityException(System.Reflection.MethodBase.GetCurrentMethod().Name + "() unsuccessfully executed by " + Thread.CurrentPrincipal.Identity.Name.Split('\\')[1]);
			}
		}

	}
}


