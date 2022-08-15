using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using ABI_RC.Core.Savior;
using System.Net.Http;
using WebSocketSharp;
using System.Reflection;
using Nocturnal.Utils;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace Nocturnal
{
    internal class MessageHandler
    {
        private static int s_code { get; set; }

        private static Json.PayLoad s_payLoade { get; set; }
        internal static Task SendLogInInfo(string Pin)
        {
            string result = WebReqManager.SendPostReq("https://napi.nocturnal-client.xyz/AccountHandler", new Utils.Json.PayLoad()
            {
                PasswordCode = Pin,
                UserId = MetaPort.Instance.ownerId,
                WebReq = new Json.WebReq()
                {
                    Username = MetaPort.Instance.username,
                    AccessKey = typeof(ABI_RC.Core.Networking.API.ApiConnection).GetField("_accessKey", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null).ToString()
                }
           });
            MelonLoader.MelonLogger.Msg(result);
            if (!result.StartsWith("Pin Setted To The Current Id")) return null;
            s_payLoade = JsonConvert.DeserializeObject<Json.PayLoad>(File.ReadAllText(Directory.GetCurrentDirectory() + "//Nocturnal//PlatesLogInfo.json"));
            s_payLoade.PasswordCode = Pin;
            Main.Pin = Pin;
            File.WriteAllText(Directory.GetCurrentDirectory() + "//Nocturnal//PlatesLogInfo.json",JsonConvert.SerializeObject(s_payLoade));
            s_payLoade = null;
            return null;
        }
        internal static void OnMessageRecived(object sender, MessageEventArgs e)
        {
            s_code = int.Parse(e.Data.Substring(9, 1));
            switch (s_code)
            {
                case 1:
                    UserChecks.Instance.RecivedErrorAuth();
                    break;
                case 2:
                
                    break;
            }
        }
        internal static void SocketConnected(object sender, EventArgs e) => UpdateTags();
        internal static void UpdateTags()
        {
            Utils.WebScoketHandler.Instance.SendMessage(JsonConvert.SerializeObject(new Json.PayLoad()
            {
                Code = 1,
                PasswordCode = Main.Pin,
                UserId = MetaPort.Instance.ownerId,
                UserTags = new Json.Tags() { TagArr = Main.Tags }

            }));
        }


    }
}
