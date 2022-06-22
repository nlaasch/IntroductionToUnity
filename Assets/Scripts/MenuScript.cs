using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public MapGenerator mapGenerator;
    public Vector2 scrollEffect;
    public TMP_InputField seedInput;
    private void Update()
    {
        mapGenerator.offset += scrollEffect;
        mapGenerator.GenerateMap();
    }


    public void PlayGame()
    {
        SeedHolder.seed = seedInput.text.Length > 0 ? Convert.ToInt32(seedInput.text) : UnityEngine.Random.Range(0, int.MaxValue);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
}
