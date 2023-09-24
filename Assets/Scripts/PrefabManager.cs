using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    [SerializeField] private string spawnTag;
    [SerializeField] private GameObject[] mobs;


    // Start is called before the first frame update
    void Start()
    {
        GameObject[] spawnObjects = GameObject.FindGameObjectsWithTag(spawnTag);

        List<GameObject> spawnList = new List<GameObject>();

        foreach (GameObject obj in spawnObjects){
            spawnList.Add(obj);
        }

        foreach (GameObject mob in mobs){
            int random_spawn = Random.Range(0,spawnList.Count);
            Vector3 spawnPos = spawnList[random_spawn].transform.position;  
            Quaternion spawnQuar = spawnList[random_spawn].transform.rotation;        
            Instantiate(mob, spawnPos, spawnQuar);
            spawnList.RemoveAt(random_spawn);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
