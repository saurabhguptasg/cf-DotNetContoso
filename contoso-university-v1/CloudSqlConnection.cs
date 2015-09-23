using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using ContosoUniversity.Logging;

namespace ContosoUniversity
{
    public class CloudSqlConnection
    {
        private static Logger LogWriter = new Logger();
        private static String actualConnection = String.Empty;

        public static String ConnectionString
        {
            get
            {
                if (actualConnection == String.Empty)
                {
                    loadConnectionString();
                }
                return actualConnection;
            }
        }

        private static void loadConnectionString()
        {
            String vcapServicesEnvVariable = Environment.GetEnvironmentVariable("VCAP_SERVICES");
            LogWriter.Information(">>> CloudSqlConnection: vcap service string: " + vcapServicesEnvVariable);
            if (vcapServicesEnvVariable != null)
            {
                Dictionary<string, object> vcapServices = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(vcapServicesEnvVariable);

                if (vcapServices != null)
                {
                    var credentialList = (Newtonsoft.Json.Linq.JArray)vcapServices["user-provided"];
                    foreach(var currCredentials in credentialList)
                    {
                        var credentialDict = (Newtonsoft.Json.Linq.JObject)currCredentials["credentials"];
                        if(credentialDict != null)
                        {
                            actualConnection = (String)credentialDict["connectionString"];
                            if(actualConnection != null)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}