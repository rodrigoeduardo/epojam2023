using UnityEngine;


public class Battery : ItemsEffects
{
    public override void RunEffect()
    {
        // Aumenta o raio de luz
        UnityEngine.Rendering.Universal.Light2D test = light.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        test.pointLightOuterRadius = test.pointLightOuterRadius*2f;


        // Implemente o código para aumentar o raio de luz aqui
    }
}
