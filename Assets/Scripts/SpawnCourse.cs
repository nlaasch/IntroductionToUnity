using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCourse : MonoBehaviour
{
    public int spawnAmount = 10;
    public GameObject checkpoint;
    public Vector3 spread;
    
    
    void Start()
    {
        Random.seed = SeedHolder.seed;
        SpawnLevel();
    }

    public void SpawnLevel()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            var randPosition = new Vector3(Random.Range(-spread.x, spread.x), Random.Range(-spread.y, spread.y),
                Random.Range(-spread.z, spread.z));
            var randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            var clone = Instantiate(checkpoint, transform.position + randPosition, randomRotation);
            clone.tag = "Checkpoint";
            clone.transform.parent = this.transform;
            // Somehow check if the clone collides with the world

        }
    }
    
}
