using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIA.Chapter12.Scripts
{
    public class InventoryPopup : MonoBehaviour
    {
        [SerializeField] private Image[] itemIcons;
        [SerializeField] private TMP_Text[] itemLabels;
        [SerializeField] private TMP_Text currItemLabel;
        [SerializeField] private Button equipButton;
        [SerializeField] private Button useButton;

        private string currItem;

        public void Refresh()
        {
            List<string> items = Managers.Inventory.Items();
            int len = Mathf.Min(items.Count, itemIcons.Length);

            for (int i = 0; i < len; ++i)
            {
                string item = items[i];

                itemIcons[i].gameObject.SetActive(true);
                itemLabels[i].gameObject.SetActive(true);

                Sprite sprite = Resources.Load<Sprite>($"Icons/{item.ToLower()}");
                itemIcons[i].sprite = sprite;
                itemIcons[i].SetNativeSize();

                int count = Managers.Inventory.ItemCount(item);
                itemLabels[i].text = (Managers.Inventory.equippedItem == item)
                    ? $"Equipped\nx{count}"
                    : $"x{count}";

                EventTrigger.Entry entry = new();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((_ => OnItem(item)));

                EventTrigger trigger = itemIcons[i].GetComponent<EventTrigger>();
                trigger.triggers.Clear();
                trigger.triggers.Add(entry);
            }

            for (int i = len; i < itemIcons.Length; ++i)
            {
                itemIcons[i].gameObject.SetActive(false);
                itemLabels[i].gameObject.SetActive(false);
            }

            if (!items.Contains(currItem))
                currItem = null;

            if (currItem is null)
            {
                currItemLabel.gameObject.SetActive(false);
                equipButton.gameObject.SetActive(false);
                useButton.gameObject.SetActive(false);
            }
            else
            {
                currItemLabel.gameObject.SetActive(true);
                equipButton.gameObject.SetActive(true);
                equipButton.GetComponentInChildren<TMP_Text>().text =
                    (Managers.Inventory.equippedItem == currItem) ? "Take off" : "Equip";
                useButton.gameObject.SetActive(currItem == "Health");
                currItemLabel.text = currItem;
            }
        }

        public void OnItem(string item)
        {
            currItem = item;
            Refresh();
        }

        public void OnEquip()
        {
            Managers.Inventory.EquipItem(currItem);
            Refresh();
        }

        public void OnUse()
        {
            Managers.Inventory.ConsumeItem(currItem);
            if (currItem == "Health")
                Managers.Player.ChangeHealth(25);
            Refresh();
        }
    }
}