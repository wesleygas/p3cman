using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour {

    public float speed = 8;
    private bool energized;
    public float delay = 3;
    public float blink = 0.5f;
    public int blinkRepetitions = 5;
    public ScoreManager scoreManager;
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start () {
        energized = false;
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update () {
        // Get user input
        float x = Input.GetAxis ("Horizontal");
        float z = Input.GetAxis ("Vertical");

        transform.Rotate (0, x * speed * 10 * Time.deltaTime, 0);

        Vector3 move = transform.forward * z;
        gameObject.GetComponent<CharacterController> ().Move (move * Time.deltaTime * speed);

    }

    void OnTriggerEnter (Collider other) {

        if (other.gameObject.tag == "Pellet") {
            scoreManager.AddScore (1);
            audioManager.Play("pellet");
            Destroy(other.gameObject);
        } else if (other.gameObject.tag == "Energizer") {
            Destroy (other.gameObject);
            scoreManager.AddScore(5);
            StartCoroutine (Energizer ());
        }else if (other.gameObject.tag == "Fruit")
        {
            Destroy(other.gameObject);
            scoreManager.AddScore(50);
            audioManager.Play("fruit");
        }
        else if (other.gameObject.tag == "Ghost") {
            if (energized) {
                Destroy (other.gameObject);
                audioManager.Play("pellet");
            } else {
                var gameManager = GameObject.FindGameObjectsWithTag ("Cube") [0].gameObject;
                gameManager.SendMessage ("pacmanHit");
            }

        }
    }

    IEnumerator Energizer () {
        Debug.Log("Starting enrgizer");
        float steady = delay - blink * blinkRepetitions;
        audioManager.Play("Energized");
        audioManager.SetTempVolume("background", 0f);
        GameObject[] ghosts = GameObject.FindGameObjectsWithTag ("Ghost");
        energized = true;
        if (steady > 0) {
            ColorGhosts (ghosts, Color.blue);
            yield return new WaitForSeconds (steady);
            ColorGhosts (ghosts, Color.white);
        }
        for (var i = 0; i < blinkRepetitions; i++) {
            ColorGhosts (ghosts, Color.blue);
            yield return new WaitForSeconds (blink);
            ColorGhosts (ghosts, Color.white);
            yield return new WaitForSeconds (blink);
        }
        energized = false;
        audioManager.Fade("Energized", 2f, 0f, true);
        audioManager.SetTempVolume("background", 0.2f);
    }

    void ColorGhosts (GameObject[] ghosts, Color color) {
        foreach (GameObject ghost in ghosts) {
            if (ghost != null) {
                ghost.GetComponent<MeshRenderer> ().material.color = color;
            }
        }
    }

}