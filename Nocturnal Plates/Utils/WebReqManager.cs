using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Nocturnal.Utils
{
    internal class WebReqManager
    {
        private static HttpWebRequest s_http { get; set; }
        private static byte[] s_byteArr { get; set; }
        private static System.IO.Stream s_stream { get; set; }


        public static string SendPostReq(string url, object body)
        {
            s_http = (HttpWebRequest)WebRequest.Create(url);
            s_http.Accept = "application/json";
            s_http.ContentType = "application/json";
            s_http.Method = "POST";
            s_byteArr = new ASCIIEncoding().GetBytes(JsonConvert.SerializeObject(body));
            s_stream = s_http.GetRequestStream();
            s_stream.Write(s_byteArr, 0, s_byteArr.Length);
            s_stream.Close();
            using (System.IO.StreamReader reader = new System.IO.StreamReader(s_http.GetResponse().GetResponseStream(), ASCIIEncoding.ASCII))
                return reader.ReadToEnd();
        }

    }
}
