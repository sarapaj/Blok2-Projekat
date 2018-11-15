using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;

namespace Manager
{
	public class ClientCertValidator : X509CertificateValidator
	{
        //Custom valiudacija - kliejnt proverava da li je sertifikad self-sign
        public override void Validate(X509Certificate2 certificate)
		{
			if (!certificate.Subject.Equals(certificate.Issuer))
			{
				throw new Exception("Certificate is not self-issued.");
			}
			else
			{
				Console.WriteLine("Certificate {0} is self-issued", certificate.SubjectName);
			}
		}
	}
}
