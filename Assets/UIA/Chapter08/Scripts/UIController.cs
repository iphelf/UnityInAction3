using UnityEngine;

namespace UIA.Chapter08.Scripts
{
    public class UIController : MonoBehaviour
    {
        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}