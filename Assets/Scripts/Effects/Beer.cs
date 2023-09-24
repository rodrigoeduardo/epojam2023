using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Beer : ItemsEffects
{
    public override void RunEffect()
    {
        print("Pisca a luz");
        AudioManager.instance.PlayAudio(sound);
        Destroy(gameObject);
    }
}
