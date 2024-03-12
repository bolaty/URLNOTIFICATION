using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace UrlNotificationCinetPayPayIn.Models
{
    public class ServicePointManagerX509Helper : IDisposable
    {
        private readonly SecurityProtocolType _originalProtocol;

        public ServicePointManagerX509Helper()
        {
            _originalProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.ServerCertificateValidationCallback += TrustingCallBack;
        }

        public void Dispose()
        {
            ServicePointManager.SecurityProtocol = _originalProtocol;
            ServicePointManager.ServerCertificateValidationCallback -= TrustingCallBack;
        }

        private bool TrustingCallBack(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // The logic for acceptance of your certificates here
            return true;
        }
    }
}
