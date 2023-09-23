using UnityEngine;

public abstract class ItemsEffects : MonoBehaviour
{
    [SerializeField] protected GameObject light;
    public abstract void RunEffect();
}
