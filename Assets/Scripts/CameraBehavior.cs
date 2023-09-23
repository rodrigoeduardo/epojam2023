using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{    
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject light;


    [SerializeField] private float accXConst;
    [SerializeField] private float accYConst;

    [SerializeField] private float leftLimit, rightLimit, topLimit, botLimit;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        UnityEngine.Vector3 cameraPos = camera.transform.position;
        
        camera.transform.position = cameraPos + getCameraAcceleration(mousePos, cameraPos);        
    }

    

    UnityEngine.Vector3 getCameraAcceleration(UnityEngine.Vector3 mousePos, UnityEngine.Vector3 cameraPos){
        float deltaX = mousePos.x - cameraPos.x;
        float deltaY = mousePos.y - cameraPos.y;

        float accX, accY, accZ=0;

        bool onRightLimit = cameraPos.x<rightLimit;
        bool onLeftLimit = cameraPos.x>leftLimit;
        bool onTopLimit = cameraPos.y<topLimit;
        bool onBotLimit = cameraPos.y>botLimit;

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

