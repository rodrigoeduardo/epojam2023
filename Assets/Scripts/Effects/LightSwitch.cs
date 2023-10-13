using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class LightSwitch : ItemsEffects
{
    [SerializeField] private Sprite on;

    private Light2D globalLight;
    public override void RunEffect() {
        if(!GameManagerSingleton.Instance.isPlayerAlive()){
            return;
        }
        this.gameObject.GetComponent<BoxCollider2D>().enabled=false;
        this.transform.Find("Canvas").gameObject.SetActive(false);
        globalLight= GameObject.Find("GLOBAL LIGHT").GetComponent<Light2D>();  

        StartCoroutine(lightsOn(globalLight));
    }

    IEnumerator lightsOn(Light2D globalLight){
        //changing sprite to "interruptorOn"
        this.GetComponent<SpriteRenderer>().sprite = on;
        //Light Animation
        globalLight.intensity=1f;
        //Light switch audio
        AudioManager.instance.PlayAudio(sound);
        yield return new WaitForSeconds(0.3f);
        globalLight.intensity=0f;
        yield return new WaitForSeconds(0.3f);
        globalLight.intensity=1f;
        yield return new WaitForSeconds(0.2f);
        globalLight.intensity=0f;
    }
}
