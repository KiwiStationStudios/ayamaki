using UnityEngine;

namespace Ayamaki.Source.Shared.Inventory
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
    public class InventoryItem : ScriptableObject
    {
        public Sprite sprite;
        public string itemName;
    }
}
