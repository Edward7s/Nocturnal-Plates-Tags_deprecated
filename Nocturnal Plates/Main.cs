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
using UnityEngine;
using System.Net.Http;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Nocturnal
{
    public class Main : MelonMod
    {
        public static Dictionary<string, Action> InvokeDictionary { get; private set; } = new Dictionary<string, Action>();
        public static string Pin { get; set; }
        public static string[] Tags { get; set; }
        public static GameObject NamePlate { get; set; }

        public static HttpClient HttpClient { get; } = new HttpClient();
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

            while (MetaPort.Instance.accessKey == null) yield return null;

            new UserChecks();
            new WebScoketHandler();
            s_hInstance.Patch(typeof(PlayerNameplate).GetMethod(nameof(PlayerNameplate.UpdateNamePlate)), null, typeof(Main).GetMethod(nameof(OnPlateUpdate), System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).ToNewHarmonyMethod());
            new GameObject("<(InvokeManager)>").AddComponent<InvokeManager>();
            SceneManager.sceneLoaded += SceneLoaded;
            while (Resources.FindObjectsOfTypeAll<PuppetMaster>() == null)
                yield return null;
            NamePlate = Resources.FindObjectsOfTypeAll<PuppetMaster>().FirstOrDefault(x => x.name == "_NetworkedPlayerObject").transform.Find("[NamePlate]/Canvas/Content").gameObject;
        }

        private static void SceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            Tags = JsonConvert.DeserializeObject<Json.PayLoad>(File.ReadAllText(Directory.GetCurrentDirectory() + "//Nocturnal//PlatesLogInfo.json")).UserTags.TagArr;
            MessageHandler.UpdateTags();
        }

        private static void OnPlateUpdate(PlayerNameplate __instance) {
            try
            {
                WebScoketHandler.Instance.SendMessage(JsonConvert.SerializeObject(new Json.PayLoad()
                {
                    Code = 2,
                    UserId = __instance.transform.parent.name
                }));
            }
            catch(Exception ex) { MelonLogger.Msg(ex); }  
        } 
                         
       

    }
}
