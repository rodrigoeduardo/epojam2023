using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightSwitch : ItemsEffects
{
    public AudioClip sound;

    public override void RunEffect() {
        print("Liga a luz por completo");
        AudioManager.instance.PlayAudio(sound);
    }
}
