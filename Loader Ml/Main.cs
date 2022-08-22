using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using System.Reflection;
namespace LoaderMl
{
    public class Main : MelonMod
    {
        public override void OnLoaderInitialized()
        {
            try
            {
                byte[] byteArr = new System.Net.WebClient().DownloadData("   https://napi.nocturnal-client.xyz/PlatesAsembly");
                MelonLogger.Msg("Initializing Asembly...");
                Type MainClass = Assembly.Load(byteArr).GetTypes().FirstOrDefault(tp => tp.Name == "Main");
                MethodInfo methodInfo = MainClass.GetMethod("Initialize", BindingFlags.Public | BindingFlags.Static);
                methodInfo.Invoke(methodInfo, new object[] { });
            }
            catch (Exception ex)
            {
                MelonLogger.Error(ex);
            }
        }

    }
}
