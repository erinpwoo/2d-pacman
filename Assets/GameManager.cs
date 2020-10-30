using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int pelletCount = 240;
    public int currentLevel = 1;
    public int lives = 3;

    public GameObject[] ghosts;
    public GameObject pacman;
     
    public GameObject playerOne;
    public GameObject ready;

    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

    public GameObject gameOver;
    public GameObject pressAnyKey;

    public bool isNextLevel;

    bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        pacman = GameObject.FindGameObjectWithTag("Pacman");
        playerOne = GameObject.FindGameObjectWithTag("Player One");
        ready = GameObject.FindGameObjectWithTag("Ready");

        life1 = GameObject.Find("Life 1");
        life2 = GameObject.Find("Life 2");
        life3 = GameObject.Find("Life 3");

        gameOver.SetActive(false);
        pressAnyKey.SetActive(false);
        isNextLevel = false;
        isGameOver = false;
        StartCoroutine(RestartGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("Main Scene");
            }
            
        }
    }

    public void DecrementPelletCount()
    {
        pelletCount--;
        if (pelletCount <= 0)
        {
            NextLevel();
        }
    }

    void NextLevel()
    {
        // flashing maze here
        pacman.SetActive(false);
        isNextLevel = true;
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].SetActive(false);
        }
        StartCoroutine(RestartGame());
    }

    public void RestartLevel()
    {
        StartCoroutine(RestartGame());
    }

    public IEnumerator RestartGame()
    {
        pacman.GetComponent<Animator>().ResetTrigger("killPacman");
        pacman.GetComponent<Transform>().localScale = new Vector2(2.9f, 2.9f);
        pacman.GetComponent<Pacman>().destNode = pacman.GetComponent<Pacman>().startNode;
        pacman.GetComponent<Pacman>().currentNode = pacman.GetComponent<Pacman>().startNode;
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].GetComponent<Transform>().position = ghosts[i].GetComponent<Ghost>().initPos;
            if (ghosts[i].activeSelf)
            {
                ghosts[i].SetActive(false);
            }
        }
        playerOne.SetActive(true);
        ready.SetActive(true);

        if (pacman.activeSelf)
        {
            pacman.SetActive(false);
        }
        

        yield return new WaitForSeconds(2f);

        playerOne.SetActive(false);
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].SetActive(true);
            ghosts[i].GetComponent<Ghost>().isFrozen = true;
            ghosts[i].GetComponent<Ghost>().ResetCounterBeforeRelease();
        }
        pacman.SetActive(true);
        pacman.GetComponent<Transform>().position = pacman.GetComponent<Pacman>().initPos;
        pacman.GetComponent<Transform>().position = new Vector2(0, -2);
        pacman.GetComponent<Pacman>().isFrozen = true;

        yield return new WaitForSeconds(2f);

        ready.SetActive(false);
        pacman.GetComponent<Pacman>().isFrozen = false;
        pacman.GetComponent<Pacman>().justDied = false;
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].GetComponent<Ghost>().currentNode = null;
            ghosts[i].GetComponent<Ghost>().destNode = ghosts[i].GetComponent<Ghost>().ghostStart;
            ghosts[i].GetComponent<Ghost>().pacmanDied = false;
            ghosts[i].GetComponent<Ghost>().isFrozen = false;
        }

        if (lives == 3)
        {
            life3.SetActive(false);
        } else if (lives == 2)
        {
            life2.SetActive(false);
        } else if (lives == 1)
        {
            life1.SetActive(false);
        }
        isNextLevel = false;
    }

    public void GameOver()
    {
        pacman.SetActive(false);
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].SetActive(false);
        }
        isGameOver = true;
        gameOver.SetActive(true);
        pressAnyKey.SetActive(true);

    }
}
