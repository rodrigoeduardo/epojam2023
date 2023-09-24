using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : ItemsEffects
{
    public override void RunEffect()
    {
        UnityEngine.Rendering.Universal.Light2D light2d = light.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        light2d.pointLightOuterRadius = light2d.pointLightOuterRadius*2f;
        AudioManager.instance.PlayAudio(sound);
        Destroy(gameObject);
        
    }
}