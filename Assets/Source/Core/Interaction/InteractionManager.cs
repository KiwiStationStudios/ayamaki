
using UnityEngine;

namespace Ayamaki.Core.Interaction
{
    public class InteractionManager : MonoBehaviour
    {
        public static InteractionManager instance;
        [SerializeField] private Interactable[] currentInteractable;
        void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
