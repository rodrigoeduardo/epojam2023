using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Door : ItemsEffects
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private GameObject camera;
    [SerializeField] private char key; // [A, B, C, D...]

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
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    private void OnMouseDown()
    {
        if (!ItemsSingleton.Instance.hasKey(key)) {
            print("Não tem a chave " + key);
            return;
        };

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

    // public override void RunEffect()
    // {
    //     if (!ItemsSingleton.Instance.hasKey(key))
    //     {
    //         return;
    //     }

    //     print("Abre a porta");

    //     // Som de abrir porta
    //     // AudioManager.instance.PlayAudio(sound);
    // }
}