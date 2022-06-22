using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class DetailGenerator : MonoBehaviour
{
    [SerializeField] private GameObject raycastHelper;
    [SerializeField] private Vector3 spread;
    public int detailAmount;
    [SerializeField] private int maxIterations;
    [SerializeField] public string detailTag = "Detail";
    [SerializeField] public bool wait = true;

    private bool localSpawn = false;

    private void Start()
    {
        Random.seed = SeedHolder.seed;
        ItemSpreader();
    }


    void ItemSpreader()
    {
        for (int i = 0; i < detailAmount; i++)
        {
            localSpawn = false;
            var counter = 0;
            var randPosition = new Vector3(Random.Range(-spread.x, spread.x), transform.position.y,
                Random.Range(-spread.z, spread.z));
            var clone = Instantiate(raycastHelper, randPosition, Quaternion.identity);
            while (!localSpawn)
            {
                var pos2d = new Vector2(Random.Range(-spread.x, spread.x),
                    Random.Range(-spread.z, spread.z));
                clone.transform.position = new Vector3(pos2d.x, clone.transform.position.y, pos2d.y);
                localSpawn = clone.GetComponent<RaycastSpawner>().PositionRaycast(detailTag) || counter == maxIterations;
                counter++;
            }
        }
        
    }
}
