using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABI_RC.Core.Savior;
using Newtonsoft.Json;
using static MelonLoader.MelonLogger;

namespace Nocturnal.Utils
{
    public class UserChecks
    {
        public static UserChecks Instance { get; private set; }
        public UserChecks()
        {
            Instance = this;
            var json = JsonConvert.DeserializeObject<Json.PayLoad>(File.ReadAllText(Directory.GetCurrentDirectory() + "//Nocturnal//PlatesLogInfo.json"));
            Main.Tags = json.UserTags.TagArr;
            if (json.PasswordCode == String.Empty)
            {
                Application.EnableVisualStyles();
                Application.Run(new FormPannel());
                return;
            }
            Main.Pin = json.PasswordCode;
        }

        public void RecivedErrorAuth()
        {
            MelonLoader.MelonLogger.Error("Pin Invalid");
            Application.Run(new FormPannel());
        }


    }
}
