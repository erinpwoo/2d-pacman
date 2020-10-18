using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node[] neighbors;
    public Vector2[] directions;
    // Start is called before the first frame update
    void Start()
    {
        directions = new Vector2[neighbors.Length];

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
}
