﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacdot : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject pacman;
    public GameObject[] ghosts;
    
    void Start()
    {
        pacman = GameObject.FindWithTag("Pacman");
        ghosts = GameObject.FindGameObjectsWithTag("Ghost");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pacman"))
        {
            if (gameObject.CompareTag("Pacdot"))
            {
                pacman.GetComponent<Pacman>().score += 10;
            }
            else if (gameObject.CompareTag("Bonus Pellet"))
            {
                pacman.GetComponent<Pacman>().score += 50;
                for (int i = 0; i < ghosts.Length; i++)
                {
                    if (ghosts[i].GetComponent<Animator>().GetBool("isScatter")) {
                        ghosts[i].GetComponent<Animator>().SetTrigger("isScatterAgain");
                    } else
                    {
                        ghosts[i].GetComponent<Animator>().SetBool("isScatter", true);
                    }
                    
                }
            }
            gameObject.SetActive(false);
        }
    }
}
