using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	[Serializable]
	public class Entity
	{
		public int Id { get; set; }
		public int Region { get; set; }
		public string City { get; set; }
		public DateTime Date { get; set; }
		public int Consumption { get; set;  }

		public Entity() {}

		public Entity(int id, int region, string city, DateTime date, int consumption)
		{
			this.Id = id;
			this.Region = region;
			this.City = city;
			this.Date = date;
			this.Consumption = consumption;
		}
	}
}
