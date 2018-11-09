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
        void Read();

		[OperationContract]
		void Count();

		[OperationContract]
		void Update();

		[OperationContract]
		void AddEntity();

		[OperationContract]
		void RemoveEntity();
	}
}
