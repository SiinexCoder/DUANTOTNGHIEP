using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Star"))
        {
            score += 1;
            Destroy(other.gameObject);
        }
    }
    
    void Update()
    {
        scoreText.text=score.ToString();
    }
}
