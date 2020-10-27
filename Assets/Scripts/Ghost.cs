using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int timeBeforeRelease;
    public bool hasBeenReleased;

    public Node currentNode;
    public Node destNode;

    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetBool("isScatter", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Animator>().GetBool("isScatter") == true)
        {
            // scatter state
        } else
        {
            if (hasBeenReleased)
            {
                RegularMove();
            } else
            {
                // up-down movement within box state
                MoveVertical();
            }
        }
    }

    void MoveVertical()
    {
        transform.localPosition += (Vector3)(Vector2.up * speed * Time.deltaTime);
        if (transform.localPosition.y >= .538 && (speed > 0))
        {
            speed = -speed;
        }
        if (transform.localPosition.y <= .192 && (speed < 0))
        {
            speed = -speed;
        }
    }

    void RegularMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, destNode.transform.position, speed * Time.deltaTime);
        if (Vector2.Distance((Vector2)destNode.transform.position, (Vector2)transform.position) <= .01f)
        {

            bool hasTurned = false;
            Node temp = currentNode;
            currentNode = destNode;
            while (!hasTurned)
            {
                int num = Random.Range(0, 4);
                if (num == 0 && destNode.up && (temp != destNode.up))
                {
                    destNode = destNode.up;
                    hasTurned = true;
                } else if (num == 1 && destNode.left && (temp != destNode.left))
                {
                    destNode = destNode.left;
                    hasTurned = true;
                } else if (num == 2 && destNode.right && (temp != destNode.right))
                {
                    destNode = destNode.right;
                    hasTurned = true;
                } else if (num == 3 && destNode.down && (temp != destNode.down))
                {
                    destNode = destNode.down;
                    hasTurned = true;
                }
            }
        }
    }

    void ScatterMove()
    {

    }
}
