using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "You took: " + SeedHolder.timeScore.ToString() + " seconds!";
    }
    
}
