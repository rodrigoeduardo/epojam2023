using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Video : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator loadGame() {
        yield return new WaitForSeconds(65);
        SceneManager.LoadScene("Bedroom");
    }
}
