using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public ScoreManager scoreManager;
    void Start()
    {
        scoreManager.score=GetScoreData(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            SaveScoreData();
        }
    }
    void SaveScoreData()
    {
        PlayerPrefs.SetInt("Score",scoreManager.score);
        Debug.Log("Da luu");
        PlayerPrefs.Save();
    }
    int GetScoreData()
    {
        return PlayerPrefs.GetInt("Score");
    }
}
