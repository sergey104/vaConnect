using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vaConnect
{
    class EapPEAP
    {
        private static String ENTERPRISE_ANON_IDENT = "";

        public static WiFiConfiguration getWiFiConfiguration(String SSID, String identity, String clientCert, String privateCert, String privateCertPass, String enterpriseCaCert, int priority, bool hiddenSSID)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var str = loader.GetString("EapPEAP");
            String profileXml = String.Format(str, SSID, identity, clientCert, privateCert, privateCertPass, enterpriseCaCert, priority, hiddenSSID);

            return new WiFiConfiguration(profileXml);
        }
    }
}
