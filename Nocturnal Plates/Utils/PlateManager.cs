using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ABI_RC.Core.UI;
using UnityEngine;

namespace Nocturnal.Utils
{
    internal class PlateManager
    {
        private static GameObject s_temporaryNamePlate { get; set; }
        private static GameObject s_textMeshProGmj { get; set; }
        private static TMPro.TextMeshProUGUI _tmpPro { get; set; }

        internal static void GeneretaeMultiplePlates(string uID, string[] tags)
        {
            CohtmlHud.Instance.ViewDropText("<color=#ff2647>Nocturnal</color>", "Recived Nocturnal Plates.");
            GameObject.Find("/" + uID + "[NamePlate]/Canvas").transform.localScale = new Vector3(0.45f, 0.45f, 1);
            for (int i = 0; i < tags.Length; i++)
            {
                s_temporaryNamePlate = GameObject.Instantiate(Main.NamePlate, GameObject.Find("/" + uID + "[NamePlate]/Canvas").transform);
                s_temporaryNamePlate.transform.localPosition = new Vector3(0, + 0.195f + i * 0.11f, 0);
                s_temporaryNamePlate.transform.Find("Image").gameObject.GetComponent<UnityEngine.UI.Image>().color = new UnityEngine.Color(0,0,0,0.7f);
                GameObject.Destroy(s_temporaryNamePlate.transform.Find("Image/FriendsIndicator").gameObject);
                GameObject.Destroy(s_temporaryNamePlate.transform.Find("Image/ObjectMaskSlave").gameObject);
                GameObject.Destroy(s_temporaryNamePlate.transform.Find("Disable with Menu").gameObject);
                s_temporaryNamePlate.transform.localScale = new Vector3(0.4f, 0.4f, 1);
                s_temporaryNamePlate.transform.Find("Image").transform.localScale = new Vector3(1, 0.5f, 1);
                s_textMeshProGmj = s_temporaryNamePlate.transform.Find("TMP:Username").gameObject;
                s_textMeshProGmj.transform.localScale = new Vector3(0.58f, 0.58f, 1);
                s_textMeshProGmj.transform.localPosition = Vector3.zero;
                _tmpPro = s_textMeshProGmj.GetComponent<TMPro.TextMeshProUGUI>();
                _tmpPro.enableAutoSizing = false;
                _tmpPro.fontSize = 0.3f;
                _tmpPro.autoSizeTextContainer = true;
                _tmpPro.text = tags[i];
            }
          
        }

    }
}
