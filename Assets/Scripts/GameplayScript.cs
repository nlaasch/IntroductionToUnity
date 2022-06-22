using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayScript : MonoBehaviour
{
    public int score = 0;
    
    
    public SpawnCourse spawnCourse;
    private int maxScore;
    public TextMeshProUGUI scoreText;

    private float startTime;
    public Material checkedMaterial;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        Cursor.visible = false;
        maxScore = spawnCourse.spawnAmount;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit Collider!");
        if (other.gameObject.tag == "Checkpoint")
        {
            Destroy(other.gameObject.GetComponent<Collider>());
            foreach (Transform child in other.transform)
            {
                Debug.Log(child.name);
                child.gameObject.GetComponent<Renderer>().material = checkedMaterial;
            }
            score++;
            scoreText.text = "Score: " + score.ToString();
            if (score == maxScore)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Cursor. visible = true;
                SeedHolder.timeScore = Time.time - startTime;
            }
        }

        if (other.gameObject.tag == "Ocean")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            Cursor. visible = true;
            SeedHolder.timeScore = Time.time - startTime;
        }
    }
    
    
}
