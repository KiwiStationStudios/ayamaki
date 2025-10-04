using Ayamaki.Core.Interactables;
using UnityEngine;

[CreateAssetMenu(fileName = "AKeyCollected", menuName = "ScriptableActions/AKeyCollected")]
public class AKeyCollected : ScriptableObject, ITriggerAction
{
    public void Execute(GameObject player)
    {
        GameManager.Instance.SetLocal<string>("keyCollected", "true");
    }
}
