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
                // regular movement
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

    }

    void ScatterMove()
    {

    }
}
