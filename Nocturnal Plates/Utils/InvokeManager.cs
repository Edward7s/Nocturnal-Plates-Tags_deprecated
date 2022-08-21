using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Nocturnal.Utils
{
    public class InvokeManager : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(this);
            InvokeRepeating(nameof(CheckInvokeDictionary), -1, 4f);
        }
        private void CheckInvokeDictionary()
        {
            if (Main.InvokeDictionary.Count == 0) return;
            for (int i = 0; i < Main.InvokeDictionary.Count; i++)
                Main.InvokeDictionary.ElementAt(0).Value.Invoke();
        }

    }
}
