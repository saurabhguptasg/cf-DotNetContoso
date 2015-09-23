using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContosoUniversity.Logging;

namespace ContosoUniversity
{
    public class CloudSqlConnection
    {
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
            if (vcapServicesEnvVariable != null)
            {
                Dictionary<string, object> vcapServices = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(vcapServicesEnvVariable);

                if (vcapServices != null)
                {
                    var credentials = ((Newtonsoft.Json.Linq.JArray)vcapServices["user-provided"]).First()["credentials"];
                    if (credentials != null)
                    {
                        actualConnection = (String)credentials["connectionString"];
                    }
                }
            }

            // if (actualConnection == String.Empty) { 
            //     actualConnection = "provide default string";
            //  }                        
        }
    }
}