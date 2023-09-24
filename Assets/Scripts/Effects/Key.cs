using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Key : ItemsEffects
{
    [SerializeField] private char key;
    [SerializeField] private Sprite image;

    public override void RunEffect() {
        print("Pegou a Chave " + this.key);
        ItemsSingleton.Instance.addKey(key);
        Destroy(gameObject);
        AudioManager.instance.PlayAudio(sound);
    }

    // Getters
    public char getKey(){
        return key;
    }

    public void Start()
    {
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
