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

    private bool lockLight = false;

    private GameObject deathCanvas;

    private float constantK, cosTheta, senTheta, timeElapsed =0;

    // Start is called before the first frame update
    void Start()
    {
        Light2D lightComponent = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        startIntensity = lightComponent.intensity;

        //Coroutine da luz piscando no início 
        StartCoroutine(StartLight(lightComponent));

        /*5.43f é a distância do topo da tela até o centro. Baseado na escala testada durante a GameJam;
        para uma distância de 5.43, o raio inicial da flashligth deve ser 1;       
        float startRadius = Math.Abs(cameraTopRightEdge.y-camera.transform.position.y)/5.43f;
        GetComponent<Light2D>().pointLightOuterRadius = startRadius;*/ 

        //Tamanho da tela
        screenHeight = UnityEngine.Screen.height;
        screenWidth = UnityEngine.Screen.width;

        //Canvas da morte
        deathCanvas = camera.transform.Find("DeathScreen").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        UnityEngine.Vector3 cameraPos = camera.transform.position;

        if(GameManagerSingleton.Instance.isPlayerAlive()){
            this.transform.position = getLightPos(mousePos, cameraPos);
            if(isDrunk){

                FlashlightFailing();
                GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity=lightIntensity;
            }
        }        

        

        //Update cronometro
        timeInCurrentRoom+=Time.deltaTime;
        DyingLight();
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

    //Falha da luz quando o player fica bêbado
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

    //Lights animation when player gets killed
    private void DyingLight(){
        //Tempo de animação
        float time = 1f;

        //Quando o jogador morrer
        if(!GameManagerSingleton.Instance.isPlayerAlive() && !lockLight){
            lightIntensity=1f;
            GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity=lightIntensity;
            //Trava a luz
            lockLight=true;
            Vector3 daughter = GameManagerSingleton.Instance.getTargetDeathPosition();
            
            //Distância inicial entre a garota e a luz
            float startDistance = Mathf.Sqrt( (float)
                (Math.Pow(-transform.position.x+daughter.x,2f)+
                Math.Pow(-transform.position.y+daughter.y, 2f)));
            
            /*Constante a ser multiplicada na função velocidade para que ela consiga atingir o deslocamento desejado no tempo desejado.
            Ela é simplesmente a distância total de deslocamento divida pela derivada da velocidade sem a constanteK no intervalo
            de 0 até o tempo total desejado de movimento, q no caso é a variável time.*/ 
            constantK = startDistance/(Mathf.Cos(Mathf.PI*(2*time - 0.5f))/(2*Mathf.PI) + time);

            //Relações trigonométricas para depois decompor a velocidade nos eixos X e Y
            cosTheta = (-transform.position.x+daughter.x)/startDistance;
            senTheta = (-transform.position.y+daughter.y)/startDistance;
            
        }

        //Animação (movimento) da luz
        if(lockLight){  
            //Velocidade de movimento da lanterna até a boneca. É uma função seno pra dar uma suavidade no deslocamento               
            float lightSpeed = (Mathf.Sin(Mathf.PI*(2*timeElapsed - 0.5f)) + 1)*constantK;

            timeElapsed+=Time.deltaTime;            
            if(timeElapsed>=time){
                return;
            }

            //Move a luz até a boneca
            transform.Translate(new Vector3(lightSpeed*cosTheta*Time.deltaTime, lightSpeed*senTheta*Time.deltaTime,0f));
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(this.gameObject.transform.position, 0.5f);
    }


    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Daughter")){
            if(timeInCurrentRoom>=1.3f && GameManagerSingleton.Instance.isPlayerAlive()){
                //Bota aonde a câmera e a lanterna têm que ir quando morrer. No caso, vai até a boneca parada
                GameManagerSingleton.Instance.setTargetDeathPosition(other.transform.position);                                                
                // Perde os itens
                ItemsSingleton.Instance.clearKeys();
                //Mata o jogador
                GameManagerSingleton.Instance.killPlayer();    
                //Inicia a animação de morte da luz
                StartCoroutine(GameManagerSingleton.Instance.dieAnimation(GetComponent<UnityEngine.Rendering.Universal.Light2D>(), deathCanvas));           
            }            
        }
    }

    //Fica ligando e desligando a luz quando começa a cena
    public IEnumerator StartLight(Light2D light){
        light.intensity = 1f;
        yield return new WaitForSeconds(0.4f);
        light.intensity = 0f;
        yield return new WaitForSeconds(0.2f);
        light.intensity = 1f;      
    }
}
