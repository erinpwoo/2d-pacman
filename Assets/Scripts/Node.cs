using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node[] neighbors;
    public Vector2[] directions;
    public Node left;
    public Node right;
    public Node up;
    public Node down;
    public GameObject pacman;
    // Start is called before the first frame update
    void Start()
    {
        directions = new Vector2[neighbors.Length];
        pacman = GameObject.FindWithTag("Pacman");

        for (int i = 0; i < neighbors.Length; i++)
        {
            Node neighbor = neighbors[i];
            Vector2 temp = neighbor.transform.localPosition - transform.localPosition; // distance vect between node and neighbor

            directions[i] = temp.normalized;
        }
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
