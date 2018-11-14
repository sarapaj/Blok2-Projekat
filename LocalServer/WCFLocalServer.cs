using Common;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LocalServer
{ 
    public class WCFLocalServer : ChannelFactory<IMainService>, IMainService, IDisposable
    {
        IMainService factory;

        public WCFLocalServer(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {

            /// cltCertCN.SubjectName should be set to the client's username. .NET WindowsIdentity class provides information about Windows user running the given process
			string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);
            factory = this.CreateChannel();
        }

		public List<Entity> InitializeList(int region1, int region2)
		{
			List<Entity> temp = new List<Entity>();

			try
			{
				temp = factory.InitializeList(region1, region2);
				Console.WriteLine("Initialize() allowed.");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while trying to RemoveEntity(). {0}", e.Message);
			}

			return temp;
		}

		public void UpdateDB(List<Entity> lista, int region1, int region2)
		{
			try
			{
				factory.UpdateDB(lista, region1, region2);
				Console.WriteLine("Initialize() allowed.");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while trying to RemoveEntity(). {0}", e.Message);
			}
		}
        public void TestCommunication()
        {
            try
            {
                factory.TestCommunication();
            }
            catch (Exception e)
            {
                Console.WriteLine("[TestCommunication] ERROR = {0}", e.Message);
            }
        }

    }
}
