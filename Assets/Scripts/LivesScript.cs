using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesScript : MonoBehaviour {
    // Start is called before the first frame update
    public int lives;
    void Start () {

    }

    // Update is called once per frame
    void Update () {

        var objects = GameObject.FindGameObjectsWithTag ("Cube");

        if (objects != null && objects.Length > 0) {
            lives = objects[0].gameObject.GetComponent<GameManager> ().lives;
        }

        int children = gameObject.transform.childCount;
        for (int i = 0; i < children; i++) {
            if (i < lives) {
                gameObject.transform.GetChild (i).gameObject.SetActive (true);
            } else {
                gameObject.transform.GetChild (i).gameObject.SetActive (false);
            }
        }

    }
}