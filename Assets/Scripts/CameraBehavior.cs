using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.Rendering.Universal;

public class CameraBehavior : MonoBehaviour
{    
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject light;


    [SerializeField] private float accXConst;
    [SerializeField] private float accYConst;

    [SerializeField] private float leftLimit, rightLimit, topLimit, botLimit;
    private float leftMaxLimit, rightMaxLimit, topMaxLimit, botMaxLimit, lowerMaxBotLimit, lowerMaxRightLimit;

    [SerializeField] private bool isRoom = false;

    [SerializeField] private float lowerRightLimit, lowerBotLimit;

    private float screenHeight, screenWidth;

    private bool lockCam = false;
    private float aElipse, bElipse, alpha, constantK, cosTheta,senTheta, timeElapsed=0f;
    private bool outElipse = true;

    // Start is called before the first frame update
    void Start()
    {
        screenHeight = UnityEngine.Screen.height;
        screenWidth = UnityEngine.Screen.width;
    
    }

    // Update is called once per frame
    void Update()
    {
        
        UnityEngine.Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        UnityEngine.Vector3 cameraPos = camera.transform.position;
        

        float lightRadius = light.GetComponent<Light2D>().pointLightOuterRadius;
        leftMaxLimit = leftLimit-lightRadius;
        rightMaxLimit = rightLimit+lightRadius;
        topMaxLimit = topLimit+lightRadius;
        botMaxLimit = botLimit-lightRadius;

        lowerMaxBotLimit = lowerBotLimit-lightRadius;
        lowerMaxRightLimit = lowerRightLimit+lightRadius;
         
        //Só move a câmera com o mouse se o player estiver vivo
        if(GameManagerSingleton.Instance.isPlayerAlive()){
           camera.transform.position = cameraPos + getCameraAcceleration(mousePos, cameraPos);      
        }

        DyingCamera();
        
    }
    
    UnityEngine.Vector3 getCameraAcceleration(UnityEngine.Vector3 mousePos, UnityEngine.Vector3 cameraPos){


        UnityEngine.Vector3 cameraBotLeftEdge = camera.ScreenToWorldPoint(new UnityEngine.Vector3(0,0,0));
        UnityEngine.Vector3 cameraTopRightEdge = camera.ScreenToWorldPoint(new UnityEngine.Vector3(screenWidth,screenHeight,0));

        float cameraLeftEdge = cameraBotLeftEdge.x;
        float cameraBotEdge = cameraBotLeftEdge.y;
        float cameraRightEdge = cameraTopRightEdge.x;
        float cameraTopEdge = cameraTopRightEdge.y;


        float deltaX = mousePos.x - cameraPos.x;
        float deltaY = mousePos.y - cameraPos.y;

        float accX, accY, accZ=0;

        //Screen limits
        bool onBotLimit;
        if (cameraPos.x < lowerRightLimit && isRoom){
            onBotLimit = cameraBotEdge>lowerMaxBotLimit;
        }else{
            onBotLimit = cameraBotEdge>botMaxLimit;
            
        }

        bool onRightLimit;
        if(cameraPos.y < botLimit && isRoom){
            onRightLimit = cameraRightEdge<lowerMaxRightLimit;
        }else{
            onRightLimit = cameraRightEdge<rightMaxLimit;            
        }
        
        bool onLeftLimit = cameraLeftEdge>leftMaxLimit;
        bool onTopLimit = cameraTopEdge<topMaxLimit;
        
        //Screen movement 
        if(deltaX>1.82f && onRightLimit){
            accX = -2+(1.82f*deltaX);
        }else if(deltaX<-1.82f && onLeftLimit){
            accX = -(-2+(1.82f*(-deltaX)));
        }else{
            accX = 0;
        }

        if(deltaY>0.77f && onTopLimit){
            accY = -1+(1.3f*deltaY);
        }else if(deltaY<-0.77f && onBotLimit){
            accY = -(-1+(1.3f*(-deltaY)));
        }else{
            accY= 0;
        }

        return new UnityEngine.Vector3(accX*accXConst, accY*accYConst, accZ);

    }

    private void DyingCamera(){    

        float time = 1f;        

        if(!GameManagerSingleton.Instance.isPlayerAlive() && !lockCam){
            
            float camX = transform.position.x;
            float camY = transform.position.y;

            lockCam=true;

            UnityEngine.Vector3 cameraTopRightEdge = camera.ScreenToWorldPoint(new UnityEngine.Vector3(screenWidth,screenHeight,0));
            
            //o A da elipse vai ser 2/7 da metade do tamanho da tela na horizontal
            aElipse = (cameraTopRightEdge.x-camX)*2/7;
            //o B da elipse vai ser 2/7 da metade do tamanho da tela na vertical
            bElipse = (cameraTopRightEdge.y-camY)*2/7;

            //Pega a posição da boneca
            UnityEngine.Vector3 daughter = GameManagerSingleton.Instance.getTargetDeathPosition();
            
            //Checa se a menina já está dentro da elipse
            double elipseConstant = Mathf.Pow((daughter.x-camX)/aElipse,2)+Mathf.Pow((daughter.y-camY)/bElipse,2);            
            if(elipseConstant<=1f){
                outElipse=false;

                return;
            }

            //Inclinação da reta que liga a boneca ao centro da câmera
            alpha = (daughter.y-camY)/(daughter.x-camX);

            //Coordenadas X obtidas a partir das raízes da equação de interseção entre a elipse e a reta que liga a boneca ao centro da tela
            float x1 = +Mathf.Sqrt(1/(float) (Math.Pow(1/aElipse,2)+Math.Pow(alpha/bElipse,2))) + camX;
            float x2 = -Mathf.Sqrt(1/(float) (Math.Pow(1/aElipse,2)+Math.Pow(alpha/bElipse,2))) + camX;

            //Coordenadas Y obtidas a partir das duas raízes
            float y1 = (x1-camX)*alpha + camY;
            float y2 = (x2-camX)*alpha + camY;

            //As duas distâncias obtidas a partir das duas raízes
            float dist1 = Mathf.Sqrt((float)(Mathf.Pow(daughter.y-y1,2)+Mathf.Pow(daughter.x-x1,2)));
            float dist2 = Mathf.Sqrt((float)(Mathf.Pow(daughter.y-y2,2)+Mathf.Pow(daughter.x-x2,2)));
            
            //Distância em que a boneca está do menor ponto de interseção da reta que liga ela até o centro da câmera
            float dist = Mathf.Min(dist1,dist2);
            
            //Distância total entre o centro da câmera e a boneca
            float distTotal = Mathf.Sqrt((float)(Mathf.Pow(daughter.y-camY,2)+Mathf.Pow(daughter.x-camX,2)));

            /*Constante a ser multiplicada na função velocidade para que ela consiga atingir o deslocamento desejado no tempo desejado.
            Ela é simplesmente a distância total de deslocamento divida pela derivada da velocidade sem a constanteK no intervalo
            de 0 até o tempo total desejado de movimento, q no caso é a variável time.*/ 
            constantK = dist/(Mathf.Cos(Mathf.PI*(2*time - 0.5f))/(2*Mathf.PI) + time);

            //Relações trigonométricas para decompor a velocidade da câmera nos eixos X e Y.
            cosTheta = (daughter.x - transform.position.x)/distTotal;
            senTheta = (daughter.y - transform.position.y)/distTotal;
        }

        if(lockCam){
            //Chega se a boneca já está dentro da elipe
            if(!outElipse){
                return;
            }

            //Velocidade de movimento da câmera até a boneca atingir a elipse. É uma função seno pra dar uma suavidade no deslocamento      
            float camSpeed = (Mathf.Sin(Mathf.PI*(2*timeElapsed - 0.5f)) + 1)*constantK;
            timeElapsed+=Time.deltaTime;

            //Checa se o tempo transcorrido de animação já ultrapassou o tempo total desejado
            if(timeElapsed>=time){
                return;
            }

            //Move a câmera até a boneca atingir a borda da elipse
            transform.Translate(new UnityEngine.Vector3(camSpeed*cosTheta*Time.deltaTime, camSpeed*senTheta*Time.deltaTime,0f));
        }
    }

    //É rodado quando o player clica no botão "Tentar Novamente". Daí restarta o jogo
    public void DeathRestart(){
        StartCoroutine(GameManagerSingleton.Instance.restartGame());
        transform.Find("DeathScreen").gameObject.SetActive(false);
    }

}

