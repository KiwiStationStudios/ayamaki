using UnityEngine;

namespace Ayamaki.Core.Interactables
{
    public class InteractionManager : MonoBehaviour
    {
        public static InteractionManager instance;

        [SerializeField] private Interaction[] interactions;
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
            
        }

        void Update()
        {
            
        }
    }
}
