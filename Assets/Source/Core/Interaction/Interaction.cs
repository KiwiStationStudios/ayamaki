using System;
using UnityEngine;

namespace Ayamaki.Core.Interactables
{
    [Serializable]
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private ScriptableObject[] conditions; // Arrasta scripts que implementam ITriggerCondition
        [SerializeField] private ScriptableObject[] actions;    // Arrasta scripts que implementam ITriggerAction

        private ITriggerCondition[] _conditions;
        private ITriggerAction[] _actions;

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
            _conditions = Array.ConvertAll(conditions, c => (ITriggerCondition)c);
            _actions = Array.ConvertAll(actions, a => (ITriggerAction)a);
        }

        public void TryInteract(GameObject interactor)
        {
            foreach (var condition in _conditions)
                if (!condition.CheckCondition(interactor))
                    return; // Alguma falhou, não executa

            Debug.Log("Interactade");

            foreach (var action in _actions)
                action.Execute(interactor);

            if (destroyOnUse)
                Destroy(gameObject);
        }
    }
}
