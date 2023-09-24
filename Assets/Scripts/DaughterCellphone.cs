using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaughterCellphone : MonoBehaviour
{
    private GameObject daughter;
    // Start is called before the first frame update
    void Start()
    {
        daughter = GameObject.Find("Daughter");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = daughter.transform.position;
    }
}
