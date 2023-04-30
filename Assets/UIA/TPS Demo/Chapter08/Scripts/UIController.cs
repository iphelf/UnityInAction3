using System;
using System.Collections.Generic;
using UIA.TPS_Demo.Chapter09.Scripts;
using UnityEngine;

namespace UIA.TPS_Demo.Chapter08.Scripts
{
    [RequireComponent(typeof(InputController))]
    public class UIController : MonoBehaviour
    {
        public Vector2 hudPosition = new(10, 10);
        public Vector2 itemSize = new(100, 30);
        public Vector2 itemListPadding = new(10, 10);

        private InputController _input;

        void Start()
        {
            _input = GetComponent<InputController>();
        }

        private void Update()
        {
            if (_input.ConfigButtonDown())
            {
                _input.SwitchMode(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            if (_input.ConfigButtonUp())
            {
                _input.SwitchMode(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        private void OnGUI()
        {
            Vector2 position = hudPosition;

            List<string> items = Managers.Inventory.Items();
            foreach (var item in items)
            {
                Texture2D image = Resources.Load<Texture2D>($"Icons/{item}");
                GUI.Box(new Rect(position, itemSize), new GUIContent($"({Managers.Inventory.ItemCount(item)})", image));
                position.x += itemSize.x + itemListPadding.x;
            }

            if (position == hudPosition)
                GUI.Box(new Rect(position, itemSize), "No Items");

            string equipped = Managers.Inventory.equippedItem;
            if (equipped is not null)
            {
                Texture2D image = Resources.Load<Texture2D>($"Icons/{equipped}");
                position.x = Screen.width - (itemSize.x + itemListPadding.x);
                GUI.Box(new Rect(position, itemSize), new GUIContent("Equipped", image));
            }

            position.x = hudPosition.x;
            position.y = hudPosition.y + itemSize.y + itemListPadding.y;
            foreach (var item in items)
            {
                if (GUI.Button(new Rect(position, itemSize), (equipped == item ? "Take off" : "Equip") + $" {item}"))
                    Managers.Inventory.EquipItem(item);
                if (item == "Health")
                {
                    if (GUI.Button(new Rect(position.x, position.y + itemSize.y + itemListPadding.y,
                            itemSize.x, itemSize.y), "Use Health"))
                    {
                        Managers.Inventory.ConsumeItem("Health");
                        Managers.Player.ChangeHealth(25);
                    }
                }

                position.x += itemSize.x + itemListPadding.x;
            }
        }
    }
}