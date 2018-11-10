using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface ILocalService
    {
        [OperationContract]
		List<Entity> Read();

		[OperationContract]
		double CountAvg(int region);

		[OperationContract]
		bool Update(int region, int month, int value);

		[OperationContract]
		bool AddEntity(Entity entity);

		[OperationContract]
		bool RemoveEntity(Entity entity);
	}
}
