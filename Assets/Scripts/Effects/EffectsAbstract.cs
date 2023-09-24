using UnityEngine;
using UnityEngine.UI;

public abstract class EffectsAbstract : MonoBehaviour
{
    public AudioClip sound;
    [SerializeField] protected GameObject light;
    [SerializeField] protected Button getButton;

    public abstract void RunEffect();
}