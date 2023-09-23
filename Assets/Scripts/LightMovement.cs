using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovement : MonoBehaviour
{
    [SerializeField] private Camera camera;

    [SerializeField] private float maxDeltaX;
    [SerializeField] private float maxDeltaY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        UnityEngine.Vector3 cameraPos = camera.transform.position;

        this.transform.position = getLightPos(mousePos, cameraPos);
    }

    UnityEngine.Vector3 getLightPos(UnityEngine.Vector3 mousePos, UnityEngine.Vector3 cameraPos){        
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
}
