using System;
using System.Security.Cryptography.X509Certificates;

namespace XCR
{
    public class X509CertInfo
    {
        String __certFile;
        String __certPassword;
        String __findValue;
        StoreLocation __storeLocation;
        StoreName __storeName;
        X509FindType __findType;
        Boolean __isFromStore;

        String __issuer;
        String __recipient;

        //string certFile, string certPassword, StoreName storeName, StoreLocation storeLocation, X509FindType findType, string findValue
        ///<summary>
        ///<para>Construtor</para>
        ///</summary>
        public X509CertInfo(Boolean isInstalled)
        {
            __isFromStore = isInstalled;
        }

        ////public SamlImpX509CertInfo(StoreName storeName, StoreLocation storeLocation, X509FindType findType, String findValue) 
        ////{
        ////    __storeName = storeName;
        ////    __storeLocation = storeLocation;
        ////    __findType = findType;
        ////    __findValue = findValue;
        ////}

        ///<summary>
        ///<para>public property - if the certificate is installed</para>
        ///</summary>
        public Boolean IsCertificateInstalled
        {
            get { return __isFromStore; }
            set { __isFromStore = value; }
        }

        ///<summary>
        ///<para>public property - path of certificate file</para>
        ///</summary>
        public String CertificateFilePath
        {
            get { if (__certFile == null) return String.Empty; else return __certFile; }
            set { __certFile = value; }
        }

        ///<summary>
        ///<para>public property - access key for certificate</para>
        ///</summary>
        public String CertificateAccessKey
        {
            get { if (__certPassword == null) return String.Empty; else return __certPassword; }
            set { __certPassword = value; }
        }

        ///<summary>
        ///<para>public property - value of the certificate 'findtype'</para>
        ///</summary>
        public String FindValue
        {
            get { if (__findValue == null) return String.Empty; else return __findValue; }
            set { __findValue = value; }
        }

        ///<summary>
        ///<para>public property - location of the store where certificate is installed</para>
        ///</summary>
        public StoreLocation StoreLocation
        {
            get { return __storeLocation; }
            set { __storeLocation = value; }
        }

        ///<summary>
        ///<para>public property - name of the store where certificate is installed</para>
        ///</summary>
        public StoreName StoreName
        {
            get { return __storeName; }
            set { __storeName = value; }
        }

        ///<summary>
        ///<para>public property - identifier for searching the certificate</para>
        ///</summary>
        public X509FindType FindType
        {
            get { return __findType; }
            set { __findType = value; }
        }

        ///<summary>
        ///<para>public property - issuer of the certificate. If empty, will not validate the issuer info.</para>
        ///</summary>
        public String Issuer
        {
            get { if (__issuer == null) return String.Empty; else return __issuer; }
            set { __issuer = value; }
        }

        ///<summary>
        ///<para>public property - recipient of the certificate. If empty, will not validate recipient info.</para>
        ///</summary>
        public String Recipient
        {
            get { if (__recipient == null) return String.Empty; else return __recipient; }
            set { __recipient = value; }
        }
    }
}