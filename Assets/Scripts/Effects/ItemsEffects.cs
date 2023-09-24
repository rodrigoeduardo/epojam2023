using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsEffects : EffectsAbstract {
    public void Start() {
        // Desativa os botões inicialmente
        getButton.gameObject.SetActive(false);
    }

    public override void RunEffect() {
        // Realiza o efeito do objeto
    }

    protected void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Light"))  {
            getButton.gameObject.SetActive(true);
        }
    }

    protected void OnTriggerExit2D(Collider2D other) {
        getButton.gameObject.SetActive(false);
    }
}