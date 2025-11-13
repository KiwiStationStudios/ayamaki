using Ayamaki.Source.Shared.Collectable;
using UnityEngine;

namespace Ayamaki.Source.Shared.Inventory
{
    public class InventoryController : MonoBehaviour, ICollectable
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        [SerializeField] private InventoryItem collectedObject;
        void Start()
        {
            //InventoryManager.Instance.AddItem(collectedObject);
        }

        public void Collect()
        {
            InventoryManager.Instance.AddItem(collectedObject);
        }
    }
}
