using UnityEngine;


public class Battery : ItemsEffects
{
    public UnityEngine.Rendering.Universal.Light2D test;
    public override void RunEffect()
    {
        // Aumenta o raio de luz
        test = this.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        test.pointLightOuterRadius = test.pointLightOuterRadius*1.5f;


        // Implemente o código para aumentar o raio de luz aqui
    }
}
