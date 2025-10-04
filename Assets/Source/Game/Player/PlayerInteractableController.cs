using Ayamaki.Core.Interactables;
using System.Collections.Generic;
using UnityEngine;

namespace Ayamaki.Core.Player
{
    [RequireComponent(typeof(Collider))]
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private KeyCode interactKey = KeyCode.E;
        [SerializeField] private Collider interactTrigger; // pode ser Box, Sphere, Capsule - só precisa ser trigger

        private readonly List<Interactable> interactablesInRange = new();

        private void Awake()
        {
            if (interactTrigger == null)
                interactTrigger = GetComponent<Collider>();

            if (interactTrigger != null)
                interactTrigger.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            var interactable = other.GetComponent<Interactable>();
            if (interactable != null && !interactablesInRange.Contains(interactable))
            {
                interactablesInRange.Add(interactable);
                interactable.isOnRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var interactable = other.GetComponent<Interactable>();
            if (interactable != null && interactablesInRange.Contains(interactable))
            {
                interactablesInRange.Remove(interactable);
                interactable.isOnRange = false;
            }
        }

        void Update()
        {
            var interactable = GetClosestInteractable();
            if (interactable != null && Input.GetKey(interactKey))
            {
                interactable.TryInteract(gameObject);
            }
        }

        Interactable GetClosestInteractable()
        {
            if (interactablesInRange.Count == 0)
                return null;

            Interactable closest = null;
            float minDist = float.MaxValue;

            foreach (var interactable in interactablesInRange)
            {
                if (interactable == null) continue;

                float dist = Vector3.Distance(transform.position, interactable.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = interactable;
                }
            }

            return closest;
        }
    }
}
