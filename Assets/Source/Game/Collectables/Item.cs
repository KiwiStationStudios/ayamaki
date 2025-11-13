using UnityEngine;

namespace Ayamaki.Source.Shared.Collectable
{
    public class Item : MonoBehaviour, ICollectable
    {
        public void Collect()
        {
            Debug.Log("Item collected: " + gameObject.name);
            Destroy(gameObject);
        }
    }

}