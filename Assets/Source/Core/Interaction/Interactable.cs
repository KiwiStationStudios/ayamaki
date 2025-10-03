using System;
using UnityEngine;

namespace Ayamaki.Core.Interaction
{
    [Serializable]
    public class Interactable : MonoBehaviour
    {
        public InteractionCondition[] conditions;
        public InteractionAction[] actions;

        public void Interact(GameObject interactor)
        {
            foreach (var condition in conditions)
            {
                if (!condition.Evaluate(interactor))
                {
                    Debug.Log($"{name}: condição falhou.");
                    return;
                }
            }

            foreach (var action in actions)
            {
                action.Execute(gameObject);
            }
        }
    }
}
