using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WelcomeScript : MonoBehaviour {
    // Start is called before the first frame update
    void Start () {

    }

    void Update () {
        if (Input.GetKeyDown ("space")) {
            PlayGame ();
        }
    }
    void PlayGame () {
        SceneManager.LoadScene ("Game");
    }

}