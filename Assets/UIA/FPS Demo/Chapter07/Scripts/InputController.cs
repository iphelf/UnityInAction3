using UIA.FPS_Demo.Chapter02;
using UIA.FPS_Demo.Chapter03;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIA.FPS_Demo.Chapter07.Scripts
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private MouseLook _mouseLookPlayer;
        [SerializeField] private FPSInput _fpsInput;
        [SerializeField] private MouseLook _mouseLookCamera;
        [SerializeField] private RayShooter _rayShooter;
        [SerializeField] private EventSystem _eventSystem;

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
            _mouseLookPlayer.IgnoreInputs(!fpsEnabled);
            _fpsInput.IgnoreInputs(!fpsEnabled);
            _mouseLookCamera.IgnoreInputs(!fpsEnabled);
            _rayShooter.IgnoreInputs(!fpsEnabled);
            _eventSystem.sendNavigationEvents = !fpsEnabled;
        }
    }
}