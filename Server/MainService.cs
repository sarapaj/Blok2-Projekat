using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class MainService : IMainService
    {
		DataIO data = new DataIO();

		public List<Entity> InitializeList(int region1, int region2)
		{
			List<Entity> temp = data.DeSerializeObject<List<Entity>>("db.xml");
			List<Entity> ret = new List<Entity>();

			foreach (var item in temp)
			{
				if (item.Region == region1 || item.Region == region2)
				{
					ret.Add(item);
				}
			}

			return ret;
		}

		public void UpdateDB(List<Entity> lista, int region1, int region2)
		{
			List<Entity> tempList = data.DeSerializeObject<List<Entity>>("db.xml");

			tempList.RemoveAll(x => x.Region == region1);
			tempList.RemoveAll(x => x.Region == region2);

			foreach (var item in lista)
			{
				tempList.Add(item);
			}

			data.SerializeObject(tempList, "db.xml");
		}
	}
}
