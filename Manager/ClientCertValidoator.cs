using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace Manager
{
	public class ClientCertValidator : X509CertificateValidator
	{
        //Custom valiudacija - kliejnt proverava da li je sertifikad self-sign
        public override void Validate(X509Certificate2 certificate)
		{
            X509Certificate2 clnCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, Formatter.ParseName(WindowsIdentity.GetCurrent().Name));
            if (!certificate.Subject.Equals(clnCert.Issuer)) 
            {
                Console.WriteLine("SubjectName serverskog sertifikata: " + certificate.Subject + " CA klijentskog sertifikata: " + clnCert.Issuer);
                throw new Exception("Certificate is not self-issued.");
            }
            Console.WriteLine("SubjectName serverskog sertifikata: " + certificate.Subject + " CA klijentskog sertifikata: " + clnCert.Issuer);
            Console.WriteLine("Sertifikat je validan");
       }
	}
}
