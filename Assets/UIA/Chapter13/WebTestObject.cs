using System.Runtime.InteropServices;
using UnityEngine;

namespace UIA.Chapter13
{
    public class WebTestObject : MonoBehaviour
    {
        [DllImport("__Internal")]
        public static extern void JsAlert(string message);

        public void RespondToBrowser(string message)
        {
            Debug.Log(message);
            JsAlert($"Unity received \"{message}\" from browser!");
        }
    }
}