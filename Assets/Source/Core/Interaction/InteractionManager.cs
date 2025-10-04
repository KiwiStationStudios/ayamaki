using UnityEngine;

namespace Ayamaki.Core.Interactables
{
    public class InteractionManager : MonoBehaviour
    {
        public static InteractionManager instance;

        [SerializeField] private Interactable[] interactables = new Interactable[0];
        public Interactable[] Interactables => interactables;

        void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
        }

        void Start()
        {
            // Pega todos os Interactables da cena ao iniciar
            interactables = FindObjectsByType<Interactable>(FindObjectsSortMode.None);
        }

        public void RegisterInteractable(Interactable interactable)
        {
            if (System.Array.IndexOf(interactables, interactable) == -1)
            {
                var newArray = new Interactable[interactables.Length + 1];
                interactables.CopyTo(newArray, 0);
                newArray[interactables.Length] = interactable;
                interactables = newArray;
            }
        }

        public void UnregisterInteractable(Interactable interactable)
        {
            int index = System.Array.IndexOf(interactables, interactable);
            if (index >= 0)
            {
                var newArray = new Interactable[interactables.Length - 1];
                if (index > 0)
                    System.Array.Copy(interactables, 0, newArray, 0, index);

                if (index < interactables.Length - 1)
                    System.Array.Copy(interactables, index + 1, newArray, index, interactables.Length - index - 1);

                interactables = newArray;
            }
        }
    }
}
