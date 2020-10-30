using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float timeBeforeRelease;
    public float timeUntilScatterEnds;
    public bool hasBeenReleased;

    public Node currentNode;
    public Node destNode;

    public float speed;
    public Node homeBase; // each ghost has its own corner it retreats to in scatter mode

    private bool hasStartedScatter;
    public bool pacmanDied;

    public bool isFrozen;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetBool("isScatter", false);
        hasStartedScatter = false;
        pacmanDied = false;
        isFrozen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pacmanDied && !isFrozen)
        {
            if (hasBeenReleased)
            {
                if (GetComponent<Animator>().GetBool("isScatter") == true && hasBeenReleased)
                {
                    // scatter state
                    GetComponent<Animator>().SetFloat("DirX", 0);
                    GetComponent<Animator>().SetFloat("DirY", 0);

                    if (!hasStartedScatter)
                    {
                        destNode = currentNode;
                        timeUntilScatterEnds = 7f;
                        hasStartedScatter = true;
                    }

                    ScatterMove();

                }
                else
                {
                    RegularMove();
                }
            }
            else
            {
                // up-down movement within box state
                MoveVertical();
                if (timeBeforeRelease > 0)
                {
                    timeBeforeRelease -= Time.deltaTime;
                }
                else
                {
                    if (speed < 0)
                    {
                        speed = -speed;
                    }
                    hasBeenReleased = true;
                }
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
        if (GetComponent<Animator>().GetBool("isScatter") == true)
        {
            if (!hasStartedScatter)
            {
                timeUntilScatterEnds = 7f;
                hasStartedScatter = true;
                GetComponent<Animator>().SetFloat("DirY", 0);
            } else {
                if (timeUntilScatterEnds > 0)
                {
                    timeUntilScatterEnds -= Time.deltaTime;
                }
                else
                {
                    GetComponent<Animator>().SetBool("isScatter", false);
                    GetComponent<Animator>().SetFloat("DirX", (Vector2.right).x);
                    GetComponent<Animator>().SetFloat("DirY", (Vector2.right).y);
                    hasStartedScatter = false;
                    timeUntilScatterEnds = 7f;
                }
            }
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
                    if (!GetComponent<Animator>().GetBool("isScatter"))
                    {
                        GetComponent<Animator>().SetFloat("DirX", (Vector2.up).x);
                        GetComponent<Animator>().SetFloat("DirY", (Vector2.up).y);
                    }
                    
                } else if (num == 1 && destNode.left && (temp != destNode.left))
                {
                    destNode = destNode.left;
                    hasTurned = true;
                    if (!GetComponent<Animator>().GetBool("isScatter"))
                    {
                        GetComponent<Animator>().SetFloat("DirX", (Vector2.left).x);
                        GetComponent<Animator>().SetFloat("DirY", (Vector2.left).y);
                    }
                } else if (num == 2 && destNode.right && (temp != destNode.right))
                {
                    destNode = destNode.right;
                    hasTurned = true;
                    if (!GetComponent<Animator>().GetBool("isScatter"))
                    {
                        GetComponent<Animator>().SetFloat("DirX", (Vector2.right).x);
                        GetComponent<Animator>().SetFloat("DirY", (Vector2.right).y);
                    }
                } else if (num == 3 && destNode.down && (temp != destNode.down))
                {
                    destNode = destNode.down;
                    hasTurned = true;
                    if (!GetComponent<Animator>().GetBool("isScatter"))
                    {
                        GetComponent<Animator>().SetFloat("DirX", (Vector2.down).x);
                        GetComponent<Animator>().SetFloat("DirY", (Vector2.down).y);
                    }
                }
            }
        }
    }

    void ScatterMove()
    {
        if (speed != .7f) speed = .7f;
        if (timeUntilScatterEnds > 0)
        {
            timeUntilScatterEnds -= Time.deltaTime;
        } else
        {
            GetComponent<Animator>().SetBool("isScatter", false);
            GetComponent<Animator>().ResetTrigger("isScatterAgain");
            timeUntilScatterEnds = 7f;
            hasStartedScatter = false;
        }
        RegularMove();
    }
}
