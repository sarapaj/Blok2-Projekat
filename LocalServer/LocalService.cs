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

		[PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Writer")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Reader")]
		public void Read()
		{
			Console.WriteLine("Usao u metodu Read()");
		}

		[PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Writer")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Reader")]
		public void Count()
		{
			Console.WriteLine("Usao u metodu Count()");
		}

		[PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Writer")]
		public void Update()
		{
			Console.WriteLine("Usao u metodu Update()");
		}

		[PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
		public void AddEntity()
		{
			Console.WriteLine("Usao u metodu AddEntity()");

		}

		[PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
		public void RemoveEntity()
		{
			Console.WriteLine("Usao u metodu RemoveEntity()");

		}
	}
}
