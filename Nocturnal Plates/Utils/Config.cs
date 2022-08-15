using System.Net;
using System.IO;
using Newtonsoft.Json;
namespace Nocturnal.Utils
{
    internal class Config
    {
        public Config()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "//Nocturnal"))
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "//Nocturnal");

            if (!File.Exists(Directory.GetCurrentDirectory() + "//Nocturnal//PlatesLogInfo.json"))
                File.WriteAllText(Directory.GetCurrentDirectory() + "//Nocturnal//PlatesLogInfo.json", JsonConvert.SerializeObject(new Json.PayLoad()
                {
                    PasswordCode = string.Empty,
                    UserTags = new Json.Tags() { TagArr = new string[] {"1:Please Change This In Settings","2: Same"} }
                }));
        }

    }
}
