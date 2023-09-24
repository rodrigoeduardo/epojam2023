using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;


public class Beer : ItemsEffects
{
    public bool drunk = false;
    public override void RunEffect()
    {
        StartCoroutine(piscar());
        //setActive false
    }

    IEnumerator piscar(){
        drunk = true;
        yield return new WaitForSeconds(5f);
        drunk=false;
        yield return new WaitForSeconds(0.3f);
        light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 1f;
    }
}
