using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManagerSingleton
{
    private static GameManagerSingleton instance;

    private Vector3 deathPosition;
    private bool playerAlive = true;

    public static GameManagerSingleton Instance{
        get{
            if(instance==null){
                instance = new GameManagerSingleton();

            }
            return instance;
        }        
    }

    //Retorna se o player está vivo
    public bool isPlayerAlive(){
        return playerAlive;
    }

    //Muda para false a variável que diz se o player está vivo
    public void killPlayer(){
        playerAlive=false;        
    }

    //Restarta o game
    public IEnumerator restartGame(){        
        yield return new WaitForSeconds(1f);
        playerAlive=true;
        SceneManager.LoadScene("Bedroom");
    }

    //Seta a posição da boneca quando o player morre
    public void setTargetDeathPosition(Vector3 deathPos){
        deathPosition = deathPos;
    }

    //Retorna a posição da boneca
    public Vector3 getTargetDeathPosition(){
        return deathPosition;
    }

    //Animação de morte: fica alternando a intensidade de luz e no fim mostra o canvas de morte
    public IEnumerator dieAnimation(Light2D light, GameObject deathCanvas){
        yield return new WaitForSeconds(1.3f);
        light.intensity = 0f;
        yield return new WaitForSeconds(0.2f);
        light.intensity = 1f;
        yield return new WaitForSeconds(0.2f);
        light.intensity = 0f; 
        yield return new WaitForSeconds(1f);  
        deathCanvas.SetActive(true);

    }

    
}
