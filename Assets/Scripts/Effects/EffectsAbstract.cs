using UnityEngine;
using UnityEngine.UI;

public abstract class EffectsAbstract : MonoBehaviour
{
    [SerializeField] protected GameObject light;
    [SerializeField] protected Button getButton;

    public abstract void RunEffect();
    public abstract void OnGetButtonClick();
}