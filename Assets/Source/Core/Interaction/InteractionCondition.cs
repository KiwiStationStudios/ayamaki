using UnityEngine;

public abstract class InteractionCondition : ScriptableObject
{
    public abstract bool Evaluate(GameObject interactor);
}