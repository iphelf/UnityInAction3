using UIA.FPS_Demo.Chapter02;
using UIA.FPS_Demo.Chapter03;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UIA.FPS_Demo.Chapter07.Scripts
{
    public class InputListenerController : MonoBehaviour
    {
        [SerializeField] private MouseLook mouseLookPlayer;

        [SerializeField] private FPSInput fpsInput;

        [SerializeField] private MouseLook mouseLookCamera;

        [SerializeField] private RayShooter rayShooter;

        [SerializeField] private EventSystem eventSystem;

        public bool fpsEnabled = true;

        // Start is called before the first frame update
        void Start()
        {
            UpdateListeners();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                fpsEnabled ^= true;
                UpdateListeners();
            }
        }

        private void UpdateListeners()
        {
            if (mouseLookPlayer)
                mouseLookPlayer.IgnoreInputs(!fpsEnabled);
            if (fpsInput)
                fpsInput.IgnoreInputs(!fpsEnabled);
            if (mouseLookCamera)
                mouseLookCamera.IgnoreInputs(!fpsEnabled);
            if (rayShooter)
                rayShooter.IgnoreInputs(!fpsEnabled);
            if (eventSystem)
                eventSystem.sendNavigationEvents = !fpsEnabled;
        }
    }
}