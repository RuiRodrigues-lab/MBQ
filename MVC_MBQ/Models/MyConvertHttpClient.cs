using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MVC_MBQ.Models
{
    /// <summary>
    /// Classe MyConvertHttpClient
    /// </summary>
    public class MyConvertHttpClient
    {
        /// <value>
        /// Propriedade que guarda o o url base.
        /// </value>
        private static string baseAddress = "https://api.neverbounce.com/v4/single/check";
        /// <value>
        /// variável local do tipo HttpClient.
        /// </value>
        private static HttpClient client;

        /// <value>
        /// Propriedade do tipo HttpClient que retorna o client do tipo HttpClient caso exista, caso não exista instancia um novo HttpClient.
        /// </value>
        public static HttpClient Client
        {
            get
            {
                if (client == null)
                {
                    client = new HttpClient();
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                }
                return client;

            }
        }
    }
}
