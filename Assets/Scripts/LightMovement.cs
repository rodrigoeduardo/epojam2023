using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovement : MonoBehaviour
{
    [SerializeField] private Camera camera;

    [SerializeField] private float maxDeltaX;
    [SerializeField] private float maxDeltaY;

    [SerializeField] private float lightIntensity = 1f;
    [SerializeField] private float nextIntensity;
    private float startIntensity;

    public bool isDrunk =false;


    // Start is called before the first frame update
    void Start()
    {
        startIntensity = GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity;

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
            nextIntensity = Random.Range(0f, 0.3f);
        }                

    } 

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(this.gameObject.transform.position, 0.5f);
    }


}
