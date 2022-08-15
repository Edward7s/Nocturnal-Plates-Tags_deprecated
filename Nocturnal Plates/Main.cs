using System;
using System.Collections.Generic;
using MelonLoader;
using Nocturnal.Utils;
using System.IO;
using ABI_RC.Core.Savior;
using System.Collections;
using ABI_RC.Core.Networking;
using Newtonsoft.Json;
using Harmony;
using ABI_RC.Core.Player;

namespace Nocturnal
{
    public class Main : MelonMod
    {
        public static Dictionary<string, Action> InvokeDictionary { get; private set; } = new Dictionary<string, Action>();
        public static string Pin { get; set; }
        public static string[] Tags { get; set; }

        [Obsolete]
        private static HarmonyInstance s_hInstance { get; } = new HarmonyInstance(Guid.NewGuid().ToString());
        [Obsolete]
        public override void OnApplicationStart() =>   
            MelonCoroutines.Start(WaitForId());

        [Obsolete]
        private static IEnumerator WaitForId()
        {
            new Config();
            while (MetaPort.Instance == null) yield return null;
            new UserChecks();
            new WebScoketHandler();
            NetworkManager.Instance.GameNetwork.Disconnected += OnDisconnect;
            s_hInstance.Patch(typeof(PlayerNameplate).GetMethod(nameof(PlayerNameplate.UpdateNamePlate)), null, typeof(Main).GetMethod(nameof(OnPlateUpdate), System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).ToNewHarmonyMethod());
        }

        private static void OnPlateUpdate(PlayerNameplate __instance) =>
            WebScoketHandler.Instance.SendMessage(JsonConvert.SerializeObject(new Json.PayLoad() { 
            Code = 2,
            UserId = __instance.transform.parent.name
            }));
                         
        private static void OnDisconnect(object sender, DarkRift.Client.DisconnectedEventArgs e)
        {
            Tags = JsonConvert.DeserializeObject<Json.PayLoad>(File.ReadAllText(Directory.GetCurrentDirectory() + "//Nocturnal//PlatesLogInfo.json")).UserTags.TagArr;
            MessageHandler.UpdateTags();
        }

    }
}
