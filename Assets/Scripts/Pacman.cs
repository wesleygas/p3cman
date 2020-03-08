using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{

    public float speed = 8;
    private bool energized;
    public float delay = 3;
    public ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        energized = false;
    }

    void Update()
    {
        // Get user input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.Rotate(0, x * speed * 10 * Time.deltaTime, 0);

        Vector3 move = transform.forward * z;
        gameObject.GetComponent<CharacterController>().Move(move * Time.deltaTime * speed);

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Pellet")
        {
            scoreManager.AddScore(1);
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "Energizer")
        {
            Destroy(other.gameObject);
            StartCoroutine(Energizer());
        }
        else if(other.gameObject.tag == "Ghost")
        {
            if (energized)
                Destroy(other.gameObject);
            else
            {

                var gameManager = GameObject.FindGameObjectsWithTag("Cube")[0].gameObject;
                gameManager.SendMessage("pacmanHit");
            }

        }
    }

    IEnumerator Energizer()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("Energized");
        audioManager.SetTempVolume("background", 0);
        GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        energized = true;
        foreach (GameObject ghost in ghosts)
        {
            if (ghost != null)
                ghost.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        yield return new WaitForSeconds(delay);
        energized = false;
        foreach (GameObject ghost in ghosts)
        {
            if (ghost != null)
                ghost.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        
        audioManager.Fade("Energized", 2, 0, true);
        audioManager.Fade("background", 2, .2f, false);
        
    }


    }
