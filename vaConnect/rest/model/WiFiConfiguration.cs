using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vaConnect
{
    public class WiFiConfiguration
    {
        public String xmlconf;
        public WiFiConfiguration() { }
        public WiFiConfiguration(String s) {
            xmlconf = s;
        }
        public string getxml() { return xmlconf; }
    }
}
