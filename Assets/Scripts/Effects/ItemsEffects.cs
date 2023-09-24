using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsEffects : EffectsAbstract {
    private void Start() {
        // Desativa os botões inicialmente
        getButton.gameObject.SetActive(false);
    }

    public override void RunEffect() {
        // Realiza o efeito do objeto
    }

    private void OnTriggerEnter2D(Collider2D other) {
        print("Trigger");
        // Verifica se o jogador entrou na área de gatilho
        if (other.CompareTag("Light"))  {
            getButton.gameObject.SetActive(true);
            print("LIGHT");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        getButton.gameObject.SetActive(false);
    }

    // Você também pode adicionar funcionalidade para quando os botões forem clicados
    public override void OnGetButtonClick()  {
        print("PEGAR");
    }
}