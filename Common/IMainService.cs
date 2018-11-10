using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
	public interface IMainService
	{
        [OperationContract]
        void Read(string username, string fileName);

		[OperationContract]
		List<Entity> InitializeList(int region1, int region2);

		[OperationContract]
		void UpdateDB(List<Entity> lista, int region1, int region2);


	}
}
