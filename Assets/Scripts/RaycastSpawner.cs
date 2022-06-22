using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSpawner : MonoBehaviour
{
    [SerializeField] private float raycastDistance = 100f;
    
    [SerializeField] private GameObject[] objectsToSpawn;
    
    private GameObject _itemToSpread;
    [Tooltip("Tags on which the Object can spawn")]
    [SerializeField] private string[] spawnableTags;

    private bool spawned = false;

    void Pick()
    {
        var randomIndex = Random.Range(0, objectsToSpawn.Length);
        _itemToSpread = objectsToSpawn[randomIndex];
    }
    
    
    
    public bool PositionRaycast(string detailTag)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {

            Pick();
            foreach(var spawnableTag in spawnableTags)
            {
                Debug.Log(spawnableTag);
                if(hit.collider.CompareTag(spawnableTag))
                {
                    Debug.Log(_itemToSpread.name);
                    var randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                    var clone = Instantiate(_itemToSpread, hit.point, randomRotation);
                    clone.tag = "Checkpoint";
                    clone.transform.parent = this.transform;
                    spawned = true;
                }
            }
            
        }

        return spawned;
    }
}
