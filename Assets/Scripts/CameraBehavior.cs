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
         
        camera.transform.position = cameraPos + getCameraAcceleration(mousePos, cameraPos);      
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

}

