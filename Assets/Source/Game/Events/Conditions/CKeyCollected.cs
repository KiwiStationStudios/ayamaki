using Ayamaki.Core.Interactables;
using UnityEngine;

[CreateAssetMenu(fileName = "CKeyCollected", menuName = "ScriptableConditions/CKeyCollected")]
public class CKeyCollected : ScriptableObject, ITriggerCondition
{
    public bool CheckCondition(GameObject player)
    {
        return GameManager.Instance.GetLocal<string>("keyCollected") == "true";
    }
}
