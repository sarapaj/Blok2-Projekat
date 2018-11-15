using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace Manager
{
	public class ServiceCertValidator : X509CertificateValidator
	{
	
        //Validacija na serverskoj strani
        //Proverava se da li je issuer klijentskog sertifikata isti kao i issuer serverskog sertifikata
		public override void Validate(X509Certificate2 certificate)
		{
			/// This will take service's certificate from storage
			X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, Formatter.ParseName(WindowsIdentity.GetCurrent().Name));

			if (!certificate.Issuer.Equals(srvCert.Issuer))
			{
				throw new Exception("Certificate is not from the valid issuer.");
			}
			else
			{
				Console.WriteLine("Certificate {0} is valid", certificate.SubjectName);
			}
		}
	}
}
