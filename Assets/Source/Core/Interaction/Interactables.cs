using System;
using Ayamaki.Core.GameAPI;
using Ayamaki.Core.GameManager;
using Lua.Unity;
using UnityEngine;

namespace Ayamaki.Core.Interactables
{
    [Serializable]
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private LuaAsset[] conditionScripts;
        [SerializeField] private LuaAsset[] actionScripts; 
        [SerializeField] private bool destroyOnUse = true;

        [Header("DebugFields")]
        public bool isOnRange = true;

        void OnEnable()
        {
            if (InteractionManager.instance != null)
                InteractionManager.instance.RegisterInteractable(this);
        }

        void OnDisable()
        {
            if (InteractionManager.instance != null)
                InteractionManager.instance.UnregisterInteractable(this);
        }

        void Awake()
        {
            //_conditions = Array.ConvertAll(conditions, c => (ITriggerCondition)c);
            //_actions = Array.ConvertAll(actions, a => (ITriggerAction)a);
        }

        public void TryInteract(GameObject interactor)
        {
            //foreach (var condition in conditions)
                //if (!condition.CheckCondition(interactor))
                    //return; // Alguma falhou, não executa

            Debug.Log("Interact");

            foreach (var script in actionScripts)
            {

            }

            // Emite o callback Lua
            LuaCallbackBus.Instance.Emit("onInteract", interactor, gameObject);

            if (destroyOnUse)
                Destroy(gameObject);
        }
    }
}
