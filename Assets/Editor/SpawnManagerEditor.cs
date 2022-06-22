using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(SpawnCourse))]
public class SpawnManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SpawnCourse spawnCourse = (SpawnCourse)target;
        if (GUILayout.Button("Spawn Course"))
        {
            target.GetComponent<SpawnCourse>().SpawnLevel();
        }

        if (GUILayout.Button("Delete Course"))
        {
            GameObject[] course = GameObject.FindGameObjectsWithTag("Checkpoint");
            foreach (var checkpoint in course)
            {
                DestroyImmediate(checkpoint);
            }
        }
    }
}