using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacdot : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject pacman;
    
    void Start()
    {
        pacman = GameObject.FindWithTag("Pacman");
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
           } else if (gameObject.CompareTag("Pacdot"))
           {
               pacman.GetComponent<Pacman>().score += 50;
           }
           gameObject.SetActive(false);
       }
    }
}
