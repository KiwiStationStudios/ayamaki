using UnityEngine;

namespace Ayamaki.Core.Interactables
{
    public interface ITriggerAction
    {
        public void Execute(GameObject interactor);
    }
}
