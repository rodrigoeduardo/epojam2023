using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBehavior : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private GameObject camera;
    [SerializeField] private char key; // [A, B, C, D...]

    public TextoBehavior textController;
    // Start is called before the first frame update
    void Start()
    {
        textController = GameObject.Find("CanvasTexto").GetComponent<TextoBehavior>();

        if (SceneManager.GetActiveScene().name == "Bedroom")
        {
            string nextDoor = PlayerPrefs.GetString("BedroomDoor");
            if (nextDoor == "DoorA")
            {
                camera.transform.position = new Vector3(-5.72f, 2.92f, -10f);
            }
            else if (nextDoor == "DoorB")
            {
                camera.transform.position = new Vector3(6.17f, 2.92f, -10f);
            }
        }
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenDoor()
    {
        if (!ItemsSingleton.Instance.hasKey(key))
        {
            textController.ShowText("Preciso encontrar a chave dessa porta...");

            Invoke("HideTextAfterDelay", 3f);
            return;
        }

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

    void HideTextAfterDelay()
    {
        textController.HideText();
    }
}
