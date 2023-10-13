using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;


public class Beer : ItemsEffects
{

    public override void RunEffect()
    {
        if(!GameManagerSingleton.Instance.isPlayerAlive()){
            return;
        }

        AudioManager.instance.PlayAudio(sound);
        StartCoroutine(piscar());
        this.gameObject.GetComponent<BoxCollider2D>().enabled=false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled=false;
        this.transform.Find("Canvas").gameObject.SetActive(false);
              
    }

    IEnumerator piscar(){
        light.GetComponent<LightMovement>().isDrunk = true;
        yield return new WaitForSeconds(5f);
        light.GetComponent<LightMovement>().isDrunk = false;
        yield return new WaitForSeconds(0.3f);
        if(GameManagerSingleton.Instance.isPlayerAlive()){
            light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 1f;
        }        
        Destroy(gameObject);
    } 
}
