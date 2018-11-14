using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security;

namespace Manager
{
    public class CertManager
    {
        //Preuzimanje sertifikata iz skladista sertifikata
        public static X509Certificate2 GetCertificateFromStorage(StoreName storeName, StoreLocation storeLocation, string subjectName)
        {
            X509Store store = new X509Store(storeName, storeLocation);
            store.Open(OpenFlags.ReadOnly);

            X509Certificate2Collection certCollection = store.Certificates.Find(X509FindType.FindBySubjectName, subjectName, true);

            ///Provera da li "subjectName" sertifikata odgovara datom "subjectName-u"
            foreach (X509Certificate2 c in certCollection)
            {
                if (c.SubjectName.Name.Equals(string.Format("CN={0}", subjectName)))
                {
                    return c;
                }
            }

            return null;
        }


        // Preuzimanje sertifikata iz fajla
        public static X509Certificate2 GetCertificateFromFile(string fileName)
        {
            X509Certificate2 certificate = null;

           
            Console.Write("Insert password for the private key: ");
            string pwd = Console.ReadLine();
         
            SecureString secPwd = new SecureString();
            foreach (char c in pwd)
            {
                secPwd.AppendChar(c);
            }
            pwd = String.Empty;

            try
            {
                certificate = new X509Certificate2(fileName, secPwd);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erroro while trying to GetCertificateFromFile {0}. ERROR = {1}", fileName, e.Message);
            }

            return certificate;
        }
    }
}
