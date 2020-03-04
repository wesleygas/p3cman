using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    int score = 0;
    int highscore = 0;
    void Start()
    {
        if (PlayerPrefs.HasKey("highscore"))
        {
            highscore = PlayerPrefs.GetInt("highscore");
        }
        gameObject.GetComponent<Text>().text = $"score: {score}/{highscore}";
    }

    // Update is called once per frame
    void Update()
    {
        if(score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
            gameObject.GetComponent<Text>().color = new Color32(0, 255, 0, 255);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        gameObject.GetComponent<Text>().text = $"score: {score}/{highscore}";
    }
}
