using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIA.FPS_Demo.Chapter11.Scripts
{
    [RequireComponent(typeof(ToggleGroup))]
    public class ToggleGroupSelector : MonoBehaviour
    {
        [Serializable]
        public class ToggleGroupSelectorEvent : UnityEvent<string>
        {
        }

        private ToggleGroup _toggleGroup;
        public ToggleGroupSelectorEvent onSelected = new();

        // Start is called before the first frame update
        void Start()
        {
            _toggleGroup = GetComponent<ToggleGroup>();
        }

        public void OnToggleChange(bool active)
        {
            if (!active) return;
            Toggle toggle = _toggleGroup.GetFirstActiveToggle();
            onSelected.Invoke(toggle.GetComponentInChildren<Text>().text);
        }
    }
}