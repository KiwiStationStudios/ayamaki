using UnityEngine;

namespace Ayamaki.Core.Interaction
{
    public class InteractionManager : MonoBehaviour
    {
        public static InteractionManager Instance;
        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
