using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ayamaki.Game.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;
        public UnityEvent<InventoryItem> onItemAdded;
        public UnityEvent<InventoryItem> onItemRemoved;
        public List<InventoryItem> slot = new();

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        public void AddItem(InventoryItem item)
        {
            slot.Add(item);
            onItemAdded.Invoke(item);
            Debug.Log("Item added");
        }

        public void RemoveItem(InventoryItem item)
        {
            slot.Remove(item);
            onItemRemoved.Invoke(item);
            Debug.Log("Item removed");
        }

        public InventoryItem GetItemIndex(int index)
        {
            if (index < 0)
                return null;

            return slot[index];
        }

        public List<InventoryItem> GetAllItems()
        {
            return slot;
        }
    }
}
