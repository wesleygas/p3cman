using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private GameObject walls; // hold pillars
    private GameObject pellets;
    public GameObject pellet;
    public GameObject energizers;
    public GameObject energizer;
    public GameObject ghosts;
    public GameObject ghost;
    public GameObject infoText;
    public int lives;
    public int defaultLives;
    private bool loaded;
    private int level;

    // 0 - Empty
    // 5 - Pellet
    // 6 - Energizer
    // 8 - Wall
    // 9 - Ghost House Door
    public static GameManager instance;
    public static readonly int[, ] map = { { 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8 },
        { 8, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 8, 8, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 8 },
        { 8, 5, 8, 8, 8, 8, 5, 8, 8, 8, 8, 8, 5, 8, 8, 5, 8, 8, 8, 8, 8, 5, 8, 8, 8, 8, 5, 8 },
        { 8, 6, 8, 8, 8, 8, 5, 8, 8, 8, 8, 8, 5, 8, 8, 5, 8, 8, 8, 8, 8, 5, 8, 8, 8, 8, 6, 8 },
        { 8, 5, 8, 8, 8, 8, 5, 8, 8, 8, 8, 8, 5, 8, 8, 5, 8, 8, 8, 8, 8, 5, 8, 8, 8, 8, 5, 8 },
        { 8, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 8 },
        { 8, 5, 8, 8, 8, 8, 5, 8, 8, 5, 8, 8, 8, 8, 8, 8, 8, 8, 5, 8, 8, 5, 8, 8, 8, 8, 5, 8 },
        { 8, 5, 8, 8, 8, 8, 5, 8, 8, 5, 8, 8, 8, 8, 8, 8, 8, 8, 5, 8, 8, 5, 8, 8, 8, 8, 5, 8 },
        { 8, 5, 5, 5, 5, 5, 5, 8, 8, 5, 5, 5, 5, 8, 8, 5, 5, 5, 5, 8, 8, 5, 5, 5, 5, 5, 5, 8 },
        { 8, 8, 8, 8, 8, 8, 5, 8, 8, 8, 8, 8, 0, 8, 8, 0, 8, 8, 8, 8, 8, 5, 8, 8, 8, 8, 8, 8 },
        { 0, 0, 0, 0, 0, 8, 5, 8, 8, 8, 8, 8, 0, 8, 8, 0, 8, 8, 8, 8, 8, 5, 8, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 8, 5, 8, 8, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 8, 8, 5, 8, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 8, 5, 8, 8, 0, 8, 8, 8, 9, 9, 8, 8, 8, 0, 8, 8, 5, 8, 0, 0, 0, 0, 0 },
        { 8, 8, 8, 8, 8, 8, 5, 8, 8, 0, 8, 0, 0, 0, 0, 0, 0, 8, 0, 8, 8, 5, 8, 8, 8, 8, 8, 8 },
        { 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0 },
        { 8, 8, 8, 8, 8, 8, 5, 8, 8, 0, 8, 0, 0, 0, 0, 0, 0, 8, 0, 8, 8, 5, 8, 8, 8, 8, 8, 8 },
        { 0, 0, 0, 0, 0, 8, 5, 8, 8, 0, 8, 8, 8, 8, 8, 8, 8, 8, 0, 8, 8, 5, 8, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 8, 5, 8, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 8, 8, 5, 8, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 8, 5, 8, 8, 0, 8, 8, 8, 8, 8, 8, 8, 8, 0, 8, 8, 5, 8, 0, 0, 0, 0, 0 },
        { 8, 8, 8, 8, 8, 8, 5, 8, 8, 0, 8, 8, 8, 8, 8, 8, 8, 8, 0, 8, 8, 5, 8, 8, 8, 8, 8, 8 },
        { 8, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 8, 8, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 8 },
        { 8, 5, 8, 8, 8, 8, 5, 8, 8, 8, 8, 8, 5, 8, 8, 5, 8, 8, 8, 8, 8, 5, 8, 8, 8, 8, 5, 8 },
        { 8, 5, 8, 8, 8, 8, 5, 8, 8, 8, 8, 8, 5, 8, 8, 5, 8, 8, 8, 8, 8, 5, 8, 8, 8, 8, 5, 8 },
        { 8, 6, 5, 5, 8, 8, 5, 5, 5, 5, 5, 5, 5, 0, 0, 5, 5, 5, 5, 5, 5, 5, 8, 8, 5, 5, 6, 8 },
        { 8, 8, 8, 5, 8, 8, 5, 8, 8, 5, 8, 8, 8, 8, 8, 8, 8, 8, 5, 8, 8, 5, 8, 8, 5, 8, 8, 8 },
        { 8, 8, 8, 5, 8, 8, 5, 8, 8, 5, 8, 8, 8, 8, 8, 8, 8, 8, 5, 8, 8, 5, 8, 8, 5, 8, 8, 8 },
        { 8, 5, 5, 5, 5, 5, 5, 8, 8, 5, 5, 5, 5, 8, 8, 5, 5, 5, 5, 8, 8, 5, 5, 5, 5, 5, 5, 8 },
        { 8, 5, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 5, 8, 8, 5, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 5, 8 },
        { 8, 5, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 5, 8, 8, 5, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 5, 8 },
        { 8, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 8 },
        { 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8 },
    };

    void Start () {
        if (instance == null) instance = this;
        else {
            Destroy (gameObject);
        }
        DontDestroyOnLoad (gameObject);
        lives = defaultLives;
        walls = new GameObject ("Walls");
        pellets = new GameObject ("Pellets");
        ghosts = new GameObject ("Ghosts");
        energizers = new GameObject ("Energizers");
        // Filling Maze
        for (int i = 0; i <= bound (0); i++) {
            for (int j = 0; j <= bound (1); j++) {
                float x = (-bound (0) / 2) + i;
                float z = (-bound (1) / 2) + j;

                if (map[i, j] == 8)
                    CreatePillar (x, z);
                else if (map[i, j] == 7) {
                    Ghost new_ghost = Instantiate (ghost, new Vector3 (x, 1, z), Quaternion.identity, ghosts.transform).GetComponent<Ghost> ();
                    new_ghost.i = i;
                    new_ghost.j = j;
                    new_ghost.gameObject.tag = "Ghost";

                } else if (map[i, j] == 5)
                    Instantiate (pellet, new Vector3 (x, 1, z), Quaternion.identity, pellets.transform);
                else if (map[i, j] == 6)
                    Instantiate (energizer, new Vector3 (x, 1, z), Quaternion.identity, energizers.transform);
            }
        }

    }

    // Update is called once per frame
    void Update () {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            int pelletCount = 0;
            var pelletChilds = pellets.transform;
            foreach(Transform child in pelletChilds)
            {
                if(child != null)
                {
                    pelletCount++;
                }
            }
            if (pelletCount <= 230)
            {
                SceneManager.LoadScene("Game");
                NextLevel(2f);
                //SceneManager.LoadScene("Status");
            }
        }

    }

    public static int bound (int dimension) {
        return map.GetUpperBound (dimension);
    }

    void CreatePillar (float x, float z) {
        GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
        cube.transform.position = new Vector3 (x, 1, z);
        cube.transform.localScale = new Vector3 (1, 2, 1);
        cube.transform.parent = walls.transform;
        cube.GetComponent<MeshRenderer> ().material.color = Color.blue;
    }

    public void pacmanHit () {

        lives -= 1;
        if (lives <= 0) {
            if (SceneManager.GetActiveScene ().name != "Status") {
                SceneManager.LoadScene ("Status");
            }
        }
    }

    public void NextLevel(float duration)
    {
        // how many seconds to pause the game
        level += 1;
        StartCoroutine(PauseGame(duration));
        infoText.GetComponent<Text>().text = $"Level {level}";

    }
    public IEnumerator PauseGame(float pauseTime)
    {
        Debug.Log("Inside PauseGame()");
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
        Debug.Log("Done with my pause");
        PauseEnded();
    }

    public void PauseEnded()
    {
        infoText.GetComponent<Text>().text = "";
    }

}