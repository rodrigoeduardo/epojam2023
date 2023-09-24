using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBehavior : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Bedroom")
        {
            string nextDoor = PlayerPrefs.GetString("BedroomDoor");
            if (nextDoor == "DoorA")
            {
                camera.transform.position = new Vector3(-3.9f, 2.92f, -10f);
            }
            else if (nextDoor == "DoorB")
            {
                camera.transform.position = new Vector3(6.17f, 2.92f, -10f);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (nextSceneName == "Bedroom")
        {
            if (SceneManager.GetActiveScene().name == "Room")
            {
                PlayerPrefs.SetString("BedroomDoor", "DoorA");
            }
            else if (SceneManager.GetActiveScene().name == "DaughterBedroom")
            {
                PlayerPrefs.SetString("BedroomDoor", "DoorB");
            }
        }

        SceneManager.LoadScene(nextSceneName);
    }
}
