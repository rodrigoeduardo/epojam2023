using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Cellphone : ItemsEffects
{
    public override void RunEffect()
    {        
        // Liga para a filha e mostra sua localização no celular
        // Implemente o código para ligar para a filha e mostrar a localização aqui
        GameObject cell = this.gameObject;
        cell.SetActive(true);
        StartCoroutine(phoneRing(cell));
        
    }

    IEnumerator phoneRing(GameObject cell){
        //COMEÇAR SOM
        
        yield return new WaitForSeconds(15f);
        cell.SetActive(false);
        //PARAR SOM

    }
}
