using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Configuration;

namespace MatixGameForm
{
    class URILocalToRemoteTranslator
    {
        private static string mToMatixGameFormatter;

        static URILocalToRemoteTranslator()
        {
            mToMatixGameFormatter = ConfigurationManager.AppSettings.Get("GameServiceFormatter");
        }

        public static Uri Translate(Uri inLocalURI)
        {
            IPHostEntry entry = Dns.GetHostEntry(Dns.GetHostName());
            if (entry.AddressList.Length > 0)
            {
                string hostName = inLocalURI.Host;
                string newURIString = inLocalURI.OriginalString;
                string newHostName = entry.AddressList[0].ToString();
                newURIString = newURIString.Replace(hostName, newHostName);
                return new Uri(newURIString);
            }
            else
                return inLocalURI;
        }

        public static Uri TranslateHostToMatixGameServiceAddress(string inHostName)
        {
            return new Uri(string.Format(mToMatixGameFormatter,inHostName));
        }
    }
}
