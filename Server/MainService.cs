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

		public void Read(string username, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
