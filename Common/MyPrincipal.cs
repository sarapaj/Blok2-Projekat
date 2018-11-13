using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class MyPrincipal : IPrincipal
	{
		Dictionary<string, List<string>> relation = new Dictionary<string, List<string>>();
		List<string> permiss = new List<string>();
		IIdentity iIdentity;

		public MyPrincipal(WindowsIdentity windowsIdentity)
		{
			Identity = windowsIdentity;

			relation.Add("Reader", new List<string>() { "Read", "CountAvg" });
			relation.Add("Writer", new List<string>() { "Read", "CountAvg", "Update" });
			relation.Add("Admin", new List<string>() { "Read", "CountAvg", "Update", "AddEntity", "RemoveEntity" });

			foreach (IdentityReference group in ((WindowsIdentity)Identity).Groups)
			{
				string name;
				SecurityIdentifier sid = (SecurityIdentifier)group.Translate(typeof(SecurityIdentifier));
				var nam = sid.Translate(typeof(NTAccount));

				if (nam.ToString().Contains(@"\"))
					name = nam.ToString().Split('\\')[1];
				else
					name = nam.ToString();

				if (relation.ContainsKey(name))
				{
					relation[name].ForEach(x => { if (!permiss.Contains(x)) { permiss.Add(x); } });
				}
			}
		}
		public IIdentity Identity
		{
			get { return iIdentity; }
			set { iIdentity = value; }
		}

		public bool IsInRole(string perm)
		{
			return permiss.Contains(perm);
		}
	}
}
