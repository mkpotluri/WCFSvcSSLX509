using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace XCR
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class TAGateway : ITAGateway
    {
        private string EncryptwithX509(string inputValue)
        {
            string __encryptedString = string.Empty;

            string __logFile = string.Empty;
            string __infoLogNeeded = string.Empty;

            try
            {
                System.Configuration.AppSettingsReader __appSettingReader = new System.Configuration.AppSettingsReader();
                __logFile = ((string)__appSettingReader.GetValue("LogFile", typeof(string)));
                __infoLogNeeded = ((string)__appSettingReader.GetValue("IsLog", typeof(string)));

                X509CertInfo __certificateInfo = new X509CertInfo(true);
                __certificateInfo.IsCertificateInstalled = true;
                __certificateInfo.StoreLocation = StoreLocation.LocalMachine;
                __certificateInfo.StoreName = StoreName.Root;
                __certificateInfo.FindType = X509FindType.FindBySerialNumber;
                __certificateInfo.FindValue = ((string)__appSettingReader.GetValue("KeyValue", typeof(string)));

                X509Certificate2 __myCertificate = Utils.GetCertificateInfo(__certificateInfo);

                if (__myCertificate.Verify())
                {
                    RSACryptoServiceProvider __rsaEncryptor = (RSACryptoServiceProvider)__myCertificate.PublicKey.Key;

                    byte[] cipherData = __rsaEncryptor.Encrypt(Encoding.UTF8.GetBytes(inputValue), true);
                    __encryptedString = Convert.ToBase64String(cipherData);
                }

                if (__infoLogNeeded.Trim().ToUpper() == "TRUE")
                    Utils.WriteToLog("Input:"+inputValue, "INFO", __logFile);
            }
            catch (Exception ex) {
                string __errorMsg ="Input:"+ inputValue + "\r\n Error:" + ex.Message.ToString();
                Utils.WriteToLog(__errorMsg, "ERROR", __logFile);
            }

            return __encryptedString;
        }

        public string OBSampleEvent(string eventData)
        {
            string __decryptedString = string.Empty;

            string __logFile=string.Empty;
            string __infoLogNeeded = string.Empty;

            try
            {
                System.Configuration.AppSettingsReader __appSettingReader = new System.Configuration.AppSettingsReader();
                __logFile= ((string)__appSettingReader.GetValue("LogFile", typeof(string)));
                __infoLogNeeded = ((string)__appSettingReader.GetValue("IsLog", typeof(string)));

                X509CertInfo __certificateInfo = new X509CertInfo(true);
                __certificateInfo.IsCertificateInstalled = true;
                __certificateInfo.StoreLocation = StoreLocation.LocalMachine;
                __certificateInfo.StoreName = StoreName.Root;
                __certificateInfo.FindType = X509FindType.FindBySerialNumber;
                __certificateInfo.FindValue = ((string)__appSettingReader.GetValue("KeyValue", typeof(string)));

                X509Certificate2 __myCertificate = Utils.GetCertificateInfo(__certificateInfo);

                if (__myCertificate.Verify())
                {
                    RSACryptoServiceProvider __rsaEncryptor = (RSACryptoServiceProvider)__myCertificate.PrivateKey;

                    byte[] __plainData = __rsaEncryptor.Decrypt((Convert.FromBase64String(eventData)), true);
                    __decryptedString = Encoding.UTF8.GetString(__plainData);
                }

                if (__infoLogNeeded.Trim().ToUpper()=="TRUE")
                    Utils.WriteToLog("Input:" + eventData, "INFO", __logFile);
            }
            catch (Exception ex) { 
                string __errorMsg = "Input:" + eventData + "\r\n Error:" + ex.Message.ToString();
                Utils.WriteToLog(__errorMsg, "ERROR",__logFile);
            }
            
            return __decryptedString;
        }
    }
}
     