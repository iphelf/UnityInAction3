using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UIA.TPS_Demo.Chapter09.Scripts
{
    public class InventoryManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus status { get; private set; }
        public string equippedItem { get; private set; }

        private Dictionary<string, int> _numberOfItem;

        public void StartUp()
        {
            Debug.Log("Inventory manager starting...");

            _numberOfItem = new Dictionary<string, int>();

            status = ManagerStatus.On;
        }

        private void DisplayItems()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Items: ");
            foreach (var (item, number) in _numberOfItem)
            {
                builder.Append(item);
                builder.Append("(");
                builder.Append(number);
                builder.Append(") ");
            }

            string display = builder.ToString();
            Debug.Log(display);
        }

        public void AddItem(string item)
        {
            _numberOfItem[item] = _numberOfItem.GetValueOrDefault(item, 0) + 1;
            DisplayItems();
        }

        public List<string> Items()
        {
            return _numberOfItem.Keys.ToList();
        }

        public int ItemCount(string item)
        {
            return _numberOfItem.GetValueOrDefault(item, 0);
        }

        // public IEnumerable<KeyValuePair<string, int>> Items()
        // {
        //     // Bad: may conflict with ConsumeItem()
        //     foreach (var (item, number) in _numberOfItem)
        //         yield return new KeyValuePair<string, int>(item, number);
        // }

        public bool EquipItem(string item)
        {
            if (_numberOfItem.ContainsKey(item) && equippedItem != item)
            {
                equippedItem = item;
                Debug.Log($"Equipped {item}");
                return true;
            }

            equippedItem = null;
            Debug.Log("Taken off");
            return false;
        }

        public bool ConsumeItem(string item)
        {
            if (_numberOfItem.ContainsKey(item))
            {
                if (--_numberOfItem[item] == 0)
                {
                    _numberOfItem.Remove(item);
                    if (equippedItem == item) EquipItem(item);
                }

                DisplayItems();
                return true;
            }

            return false;
        }
    }
}