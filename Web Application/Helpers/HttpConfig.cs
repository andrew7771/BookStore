using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Web_Application.Helpers
{
    public class HttpConfig
    {
        private const string adress = "http://localhost:54883/";

        public void SetClientGETSettings(HttpClient client)
        {
            client.BaseAddress = new Uri(adress);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public string getAdress()
        {
            return adress;
        }        
    }
}