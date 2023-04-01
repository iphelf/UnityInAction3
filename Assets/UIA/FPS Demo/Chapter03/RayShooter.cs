using System.Collections;
using UnityEngine;

namespace UIA.FPS_Demo.Chapter03
{
    [RequireComponent(typeof(Camera))]
    public class RayShooter : MonoBehaviour
    {
        private Camera cam;

        private bool _ignoreInputs = false;

        // Start is called before the first frame update
        private void Start()
        {
            cam = GetComponent<Camera>();
            captureCursor(true);
        }

        // Update is called once per frame
        private void Update()
        {
            if (_ignoreInputs) return;
            if (Input.GetButtonDown("Fire1"))
            {
                var screenCenter = new Vector3(cam.pixelWidth / 2.0f, cam.pixelHeight / 2.0f, 0.0f);
                var ray = cam.ScreenPointToRay(screenCenter);
                if (Physics.Raycast(ray, out var hit))
                {
                    ReactiveTarget target = hit.transform.gameObject.GetComponent<ReactiveTarget>();
                    if (target is null)
                    {
                        Debug.Log($"Hit at building: {hit.point}");
                        StartCoroutine(s_showTemporarySphereAt(hit.point));
                    }
                    else
                    {
                        Debug.Log($"Hit at target");
                        target.ReactToHit();
                    }
                }
            }
        }

        private void captureCursor(bool captured)
        {
            if (captured)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        private static IEnumerator s_showTemporarySphereAt(Vector3 position)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = position;
            yield return new WaitForSeconds(1.0f);
            sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            yield return new WaitForSeconds(1.0f);
            sphere.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            Destroy(sphere);
        }

        private void OnGUI()
        {
            var size = 12;
            float x = cam.pixelWidth / 2.0f + size / 2.0f;
            float y = cam.pixelHeight / 2.0f + size / 2.0f;
            GUI.Label(new Rect(x, y, size, size), "+", new GUIStyle()
            {
                alignment = TextAnchor.MiddleCenter
            });
        }

        public void IgnoreInputs(bool ignoreInputs)
        {
            _ignoreInputs = ignoreInputs;
            captureCursor(!ignoreInputs);
        }
    }
}