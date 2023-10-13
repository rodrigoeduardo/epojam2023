using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Cellphone : ItemsEffects
{
    [SerializeField] private GameObject daughterPhone;
    public override void RunEffect()
    {     
        if(!GameManagerSingleton.Instance.isPlayerAlive()){
            return;
        }

        // Liga para a filha e mostra sua localização no celular
        // Implemente o código para ligar para a filha e mostrar a localização aqui
        GameObject cell = this.gameObject;
        cell.GetComponent<BoxCollider2D>().enabled=false;

        cell.transform.Find("Canvas").gameObject.SetActive(false);
        StartCoroutine(phoneRing(cell, daughterPhone));
        AudioManager.instance.PlayAudio(sound);
                        
    }

    IEnumerator phoneRing(GameObject cell, GameObject daughterCell){
        //COMEÇAR SOM
        daughterCell.SetActive(true);
        yield return new WaitForSeconds(7f);
        daughterCell.SetActive(false);
        cell.SetActive(false);
        
        //PARAR SOM

    }
}
