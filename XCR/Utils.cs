using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace XCR
{
    public static class Utils
    {

        public static X509Certificate2 GetCertificateInfo(X509CertInfo certificateInfo)
        {
            X509Certificate2 __myCertificate = null;

            if (certificateInfo == null)
            {
                throw new Exception("Certificate is null or invalid");
            }

            try
            {
                if (certificateInfo.IsCertificateInstalled)
                {
                    X509Store store = new X509Store(certificateInfo.StoreName, certificateInfo.StoreLocation);
                    store.Open(OpenFlags.ReadOnly);
                    string _fVal = certificateInfo.FindValue as string;
                    X509Certificate2Collection coll = store.Certificates.Find(certificateInfo.FindType, _fVal, true);
                    
                    if (coll.Count < 1)
                    {
                        throw new ArgumentException("Unable to locate certificate");
                    }
                    __myCertificate = coll[0];
                    store.Close();
                }
                else
                {
                    if (System.IO.File.Exists(certificateInfo.CertificateFilePath))
                    {
                        __myCertificate = new X509Certificate2(certificateInfo.CertificateFilePath, certificateInfo.CertificateAccessKey);
                    }
                    else
                    {
                        throw new ArgumentException(" certificate file is not identified");
                    }
                }
            }
            catch (Exception certException)
            { throw new Exception(certException.Message.ToString()); }

            if (__myCertificate == null)
            {
                throw new Exception("Certificate is null or invalid");
            }

            return __myCertificate;
        }

        public static void WriteToLog(string message,string logType, string filePath)
        {
            try
            {
                string __logFile = string.Empty;

                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                __logFile = filePath+"\\"+DateTime.Now.ToString("yyyyMMdd")+".log";

                String __logInfo = "\r\n"+DateTime.Now.ToString() + ":: " + logType;
                __logInfo += "\r\n" + message+"\r\n\r\n";


                File.AppendAllText(__logFile, __logInfo);
            }
            catch (Exception) { }
            
        }
    }
}