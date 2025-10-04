using UnityEngine;

namespace Ayamaki.Core.Interactables
{
    public interface ITriggerCondition
    {
        bool CheckCondition(GameObject interactor);
    }
}
