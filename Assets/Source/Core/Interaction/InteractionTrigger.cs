using UnityEngine;

namespace Ayamaki.Core.Interaction
{
    [RequireComponent(typeof(Collider))]
    public class InteractionTrigger : MonoBehaviour
    {
        private Interactable interactable;
        void Awake()
        {
            interactable = GetComponent<Interactable>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
