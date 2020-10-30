using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int pelletCount = 265;
    public int currentLevel = 1;
    public int lives = 3;

    public GameObject[] ghosts;
    public GameObject pacman;

    public GameObject playerOne;
    public GameObject ready;

    // Start is called before the first frame update
    void Start()
    {
        ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        pacman = GameObject.FindGameObjectWithTag("Pacman");
        playerOne = GameObject.FindGameObjectWithTag("Player One");
        ready = GameObject.FindGameObjectWithTag("Ready");

        StartCoroutine(RestartGame());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator RestartGame()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].SetActive(false);
        }
        playerOne.SetActive(true);
        ready.SetActive(true);

        pacman.SetActive(false);

        yield return new WaitForSeconds(2f);

        playerOne.SetActive(false);
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].SetActive(true);
            ghosts[i].GetComponent<Ghost>().isFrozen = true;
        }
        pacman.SetActive(true);
        pacman.GetComponent<Pacman>().isFrozen = true;

        yield return new WaitForSeconds(2f);

        ready.SetActive(false);
        pacman.GetComponent<Pacman>().isFrozen = false;
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].GetComponent<Ghost>().isFrozen = false;
        }
    }
}
