using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ObjectToSpawn;
    public int ObjectCount = 100;
    int spawnedObjects = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedObjects<ObjectCount)
        {
            Instantiate(ObjectToSpawn,Random.insideUnitSphere*50,Random.rotation,transform);
            spawnedObjects++;
        }
    }
}
