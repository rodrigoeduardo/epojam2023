using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : ItemsEffects
{
    public override void RunEffect()
    {
        // Aumenta o raio de luz
        UnityEngine.Rendering.Universal.Light2D test = light.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        test.pointLightOuterRadius = test.pointLightOuterRadius*2f;
        AudioManager.instance.PlayAudio(sound);
        Destroy(gameObject);
    }
}