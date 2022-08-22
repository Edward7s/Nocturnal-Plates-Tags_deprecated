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
        private static Json.PayLoad s_payLoad { get; set; }
        internal static async Task SendLogInInfo(string Pin)
        {
            MelonLoader.MelonLogger.Msg("Sending Info");
            try
            {
                string result = await WebReqManager.SendWebReq("Post", "https://napi.nocturnal-client.xyz/AccountHandler", null, JsonConvert.SerializeObject(new Utils.Json.PayLoad()
                {
                    PasswordCode = Pin,
                    WebReq = new Json.WebReq()
                    {
                        Username = MetaPort.Instance.username,
                        AccessKey = MetaPort.Instance.accessKey,
                       // AccessKey = typeof(ABI_RC.Core.Networking.API.ApiConnection).GetField("_accessKey", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null).ToString()
                    }
                }));
                //MelonLoader.MelonLogger.Msg(result);
                if (!result.StartsWith("Pin")) return;
                s_payLoad = JsonConvert.DeserializeObject<Json.PayLoad>(File.ReadAllText(Directory.GetCurrentDirectory() + "//Nocturnal//PlatesLogInfo.json"));
                s_payLoad.PasswordCode = Pin;
                Main.Pin = Pin;
                File.WriteAllText(Directory.GetCurrentDirectory() + "//Nocturnal//PlatesLogInfo.json", JsonConvert.SerializeObject(s_payLoad));
                s_payLoad = null;
                return;
            }
            catch(Exception ex) {
                MelonLoader.MelonLogger.Msg(ex);
            }
          
        }
        internal static void OnMessageRecived(object sender, MessageEventArgs e)
        {
            try
            {
                Json.PayLoad payLoad = JsonConvert.DeserializeObject<Json.PayLoad>(e.Data);
                switch (payLoad.Code)
                {
                    case 1:
                        string key1 = Guid.NewGuid().ToString();
                        Main.InvokeDictionary.Add(key1, new Action(() => {
                            Main.InvokeDictionary.Remove(key1);
                            key1 = null;
                            UserChecks.Instance.RecivedErrorAuth();
                        }
                        ));
                        break;
                    case 2:
                        string key = Guid.NewGuid().ToString();
                        Main.InvokeDictionary.Add(key, new Action(() => {
                            Main.InvokeDictionary.Remove(key);
                            key = null;
                            PlateManager.GeneretaeMultiplePlates(payLoad.UserId, payLoad.UserTags.TagArr);
                        }
                        ));
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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
