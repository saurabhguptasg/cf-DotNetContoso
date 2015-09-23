using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;

namespace ContosoUniversity
{
    public class Utils
    {
        private static readonly String InstanceId = (new Random()).Next().ToString();

        public static String MyIPAddress
        {
            get
            {
                string hostName = Dns.GetHostName(); // Retrive the Name of HOST
                // Get the IP
                String myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                return myIP + " / instance: "+ InstanceId;
            }
        }
    }
}