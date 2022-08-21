using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using static Nocturnal.Utils.Json;

namespace Nocturnal.Utils
{
    internal class WebReqManager
    {
        private static HttpResponseMessage s_http { get; set; }
        public static async Task<string> SendWebReq(string type,string url, Dictionary<string,string> Headers = null,string Body = "")
        {
            try
            {
                for (int i = 0; i < Main.HttpClient.DefaultRequestHeaders.Count(); i++)
                    Main.HttpClient.DefaultRequestHeaders.Remove(Main.HttpClient.DefaultRequestHeaders.ElementAt(i).Key);
                if (Headers != null)
                    for (int i = 0; i < Headers.Count; i++)
                        Main.HttpClient.DefaultRequestHeaders.Add(Headers.ElementAt(i).Key, Headers.ElementAt(i).Value);
                if (type == "Post")
                    s_http = await Main.HttpClient.PostAsync(url, new StringContent(Body, Encoding.UTF8, "application/json"));
                else
                    s_http = await Main.HttpClient.GetAsync(url);
                return await s_http.Content.ReadAsStringAsync();
            }
            catch(Exception ex) {
                MelonLoader.MelonLogger.Msg(ex);
            }
            return "Failed";
        }

    }
}
