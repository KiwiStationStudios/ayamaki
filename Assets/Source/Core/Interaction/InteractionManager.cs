using UnityEngine;

namespace Ayamaki.Core.Interaction
{
    public class InteractionManager : MonoBehaviour
    {
        public static InteractionManager Instance;
        [SerializeField] private TriggerArea[] areas;


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

    class TriggerArea
    {
        public string name = "";
        public bool destroyOnTrigger = true;
    }
}
