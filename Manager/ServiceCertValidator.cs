using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.IdentityModel.Tokens;

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

            if (!certificate.Issuer.Equals(srvCert.SubjectName.Name))
            {
                Console.WriteLine("\nCA klijentskog sertifikata: " + certificate.Issuer + " SubjectName serverskog sertifikata " + srvCert.SubjectName.Name);
                throw new SecurityTokenValidationException("Sertifikat nije izdat od odgovarajuceg CA.");
               
            }
            else
            {
                Console.WriteLine("Sertifikat je validan");
                Console.WriteLine("\nCA klijentskog sertifikata: " + certificate.Issuer + " SubjectName serverskog sertifikata " + srvCert.SubjectName.Name);
            }
        }
	}
}
