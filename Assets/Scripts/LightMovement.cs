using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;


public class LightMovement : MonoBehaviour
{
    [SerializeField] private Camera camera;

    private float maxDeltaX, maxDeltaY;

    [SerializeField] private float lightIntensity = 1f;
    [SerializeField] private float nextIntensity;
    private float startIntensity;

    public bool isDrunk =false;

    private float timeInCurrentRoom =0f;

    private float screenHeight, screenWidth;


    // Start is called before the first frame update
    void Start()
    {
        startIntensity = GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity;

        UnityEngine.Vector3 cameraTopRightEdge = camera.ScreenToWorldPoint(new UnityEngine.Vector3(screenWidth,screenHeight,0));

        /*5.43f é a distância do topo da tela até o centro. Baseado na escala testada durante a GameJam;
        para uma distância de 5.43, o raio inicial da flashligth deve ser 1;       
        float startRadius = Math.Abs(cameraTopRightEdge.y-camera.transform.position.y)/5.43f;
        GetComponent<Light2D>().pointLightOuterRadius = startRadius;*/ 

        screenHeight = UnityEngine.Screen.height;
        screenWidth = UnityEngine.Screen.width;

    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        UnityEngine.Vector3 cameraPos = camera.transform.position;

        this.transform.position = getLightPos(mousePos, cameraPos);


        if(isDrunk){

            FlashlightFailing();
            GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity=lightIntensity;
        }

        //Update cronometro
        timeInCurrentRoom+=Time.deltaTime;
    }

    UnityEngine.Vector3 getLightPos(UnityEngine.Vector3 mousePos, UnityEngine.Vector3 cameraPos){   
        //Camera Position in world

        UnityEngine.Vector3 cameraTopRightEdge = camera.ScreenToWorldPoint(new UnityEngine.Vector3(screenWidth,screenHeight,0));

        //Get light radius
        UnityEngine.Rendering.Universal.Light2D light2d = this.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        float lightRadius = light2d.pointLightOuterRadius;
        maxDeltaX = Mathf.Abs(cameraTopRightEdge.x-cameraPos.x-lightRadius);
        maxDeltaY = Mathf.Abs(cameraTopRightEdge.y-cameraPos.y-lightRadius);

        float xLight;
        float yLight;
        float zLight = 0;

        float deltaX = mousePos.x - cameraPos.x;
        float deltaY = mousePos.y - cameraPos.y;

        if(deltaY>maxDeltaY){
            yLight=cameraPos.y+maxDeltaY;
        }else if (deltaY<-maxDeltaY){
            yLight=cameraPos.y-maxDeltaY;
        }else{
            yLight = mousePos.y;
        }       

        if(deltaX>maxDeltaX){
            xLight=cameraPos.x+maxDeltaX;
        }else if(deltaX<-maxDeltaX){
            xLight=cameraPos.x-maxDeltaX;
        }else{
            xLight=mousePos.x;
        }

        return new UnityEngine.Vector3(xLight,yLight,zLight);
    }

    private void FlashlightFailing()
    {
        const float coeficientSpeed = 1f;

        if (Mathf.Abs(nextIntensity - lightIntensity) > 0.03f)
        {
            if (nextIntensity > lightIntensity)
            {
                lightIntensity += Time.deltaTime*coeficientSpeed;
            }
            else
            {
                lightIntensity -= Time.deltaTime * coeficientSpeed;
            }
        }
        else
        {
            nextIntensity = UnityEngine.Random.Range(0f, 0.3f);
        }                

    } 

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(this.gameObject.transform.position, 0.5f);
    }


    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Daughter")){
            if(timeInCurrentRoom>=1.3f){
                // Perde os itens
                print("restart");
                ItemsSingleton.Instance.clearKeys();
                SceneManager.LoadScene("Bedroom");
            }
            
        }
    }


}
