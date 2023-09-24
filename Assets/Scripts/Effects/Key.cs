using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Key : ItemsEffects
{
    /*
    A: Abre a porta A
    B: Abre a porta B
    P: Pista que permite acessar a TV [T]
    T: Chave que permite pegar a chave na Cômoda [K]
    K: Libera a última porta: K
    */

    [SerializeField] private char key;
    Dictionary<char, char> keyRequired = new Dictionary<char, char>();

    [SerializeField] private Sprite image;

    public override void RunEffect() {
        bool required = keyRequired.ContainsKey(key);

        if (required) {
            char k = keyRequired[key];
            if (!ItemsSingleton.Instance.hasKey(k)){
                print("Chave necessária " + k);
                return;
            }
        }

        print("Pegou a Chave " + this.key);
        ItemsSingleton.Instance.addKey(key);
        Destroy(gameObject);
        AudioManager.instance.PlayAudio(sound);
    }

    protected void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Light"))  {
            bool required = keyRequired.ContainsKey(key);

            if (required) {
                char k = keyRequired[key];
                if (!ItemsSingleton.Instance.hasKey(k)){
                    print("Chave necessária " + k);
                    return;
                }
            }

            getButton.gameObject.SetActive(true);
        }
    }

    // Getters
    public char getKey(){
        return key;
    }

    public void Start()
    {
        keyRequired['T'] = 'P';
        keyRequired['K'] = 'T';
        getButton.gameObject.SetActive(false);
        if (image != null)
        {
            // Acesse o SpriteRenderer do GameObject atual.
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            // Defina o sprite do SpriteRenderer com o sprite da imagem.
            spriteRenderer.sprite = image;
        }
    }
}
