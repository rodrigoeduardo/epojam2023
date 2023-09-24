using UnityEngine;


public class Battery : ItemsEffects
{
    public override void RunEffect()
    {
        // Aumenta o raio de luz
        UnityEngine.Rendering.Universal.Light2D light2d = light.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        light2d.pointLightOuterRadius = light2d.pointLightOuterRadius*2f;


        // Implemente o código para aumentar o raio de luz aqui
    }
}
