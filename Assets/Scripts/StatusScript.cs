using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatusScript : MonoBehaviour {

    // Start is called before the first frame update
    private GameObject GameManager;
    void Start () {

        var objects = GameObject.FindGameObjectsWithTag ("Cube");

        if (objects != null && objects.Length > 0) {
            GameManager = objects[0].gameObject;
            var lives = GameManager.GetComponent<GameManager> ().lives;
            if (lives > 0) {
                gameObject.GetComponent<Text> ().text = "YOU WON!";
            } else {
                gameObject.GetComponent<Text> ().text = "You're dead!";
            }
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown ("space")) {
            PlayGame ();
        }
    }
    void PlayGame () {

        if (GameManager != null) {
            GameManager.GetComponent<GameManager> ().lives = GameManager.GetComponent<GameManager> ().defaultLives;
        }
        SceneManager.LoadScene ("Game");
    }
}