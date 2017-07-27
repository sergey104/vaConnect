using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vaConnect
{
    class EapTLS
    {

        private static String ENTERPRISE_ANON_IDENT = "";
       
        public static WiFiConfiguration getWiFiConfiguration(String SSID, String identity, String clientCert, String privateCert, String privateCertPass, String enterpriseCaCert, int priority, bool hiddenSSID)
        {
            String template = Properties.Resources.EapTLS;
            String profileXml = String.Format(template, SSID, identity, clientCert, privateCert, privateCertPass, enterpriseCaCert, priority, hiddenSSID);
            

            return new WiFiConfiguration(profileXml);
        }
     

    }
}