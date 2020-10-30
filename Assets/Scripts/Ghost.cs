using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float timeBeforeRelease;
    public float counterBeforeRelease;
    public float timeUntilScatterEnds;
    public bool hasBeenReleased;

    public Node currentNode;
    public Node destNode;

    public float speed;
    public Node homeBase; // each ghost has its own corner it retreats to in scatter mode

    private bool hasStartedScatter;
    public bool pacmanDied;

    public bool isFrozen;
    public bool isGoingBackToHauntedHouse;

    public Vector2 initPos;

    public Node ghostStart;

    public Sprite leftEye;
    public Sprite rightEye;
    public Sprite downEye;
    public Sprite upEye;

    public Sprite defaultSprite;
    public float eyeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetBool("isScatter", false);
        hasStartedScatter = false;
        pacmanDied = false;
        isFrozen = true;
        isGoingBackToHauntedHouse = false;
        ResetCounterBeforeRelease();
    }

    public void ResetCounterBeforeRelease()
    {
        counterBeforeRelease = timeBeforeRelease;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pacmanDied && !isFrozen && !isGoingBackToHauntedHouse)
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
                if (counterBeforeRelease > 0)
                {
                    counterBeforeRelease -= Time.deltaTime;
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
        if (isGoingBackToHauntedHouse)
        {
            SendBackToHauntedHouse();
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

    public void SendBackToHauntedHouse()
    {
        if (GetComponent<Animator>().isActiveAndEnabled)
        {
            GetComponent<Animator>().enabled = false;
        }
        
        transform.position = Vector2.MoveTowards(transform.position, destNode.transform.position, eyeSpeed * Time.deltaTime);
        if (Vector2.Distance((Vector2)destNode.transform.position, (Vector2)transform.position) <= .01f)
        {
            currentNode = destNode;
            float min = Mathf.Infinity;

            Node temp = currentNode;
            if (currentNode.left)
            {
                if (Vector2.Distance((Vector2)ghostStart.transform.position, (Vector2)currentNode.left.GetComponent<Transform>().position) <= min)
                {
                    min = Vector2.Distance((Vector2)ghostStart.transform.position, (Vector2)currentNode.left.GetComponent<Transform>().position);
                    temp = currentNode.left;
                }
            }
            if (currentNode.right)
            {
                if (Vector2.Distance((Vector2)ghostStart.transform.position, (Vector2)currentNode.right.GetComponent<Transform>().position) <= min)
                {
                    min = Vector2.Distance((Vector2)ghostStart.transform.position, (Vector2)currentNode.right.GetComponent<Transform>().position);
                    temp = currentNode.right;
                }
            }
            if (currentNode.up)
            {
                if (Vector2.Distance((Vector2)ghostStart.transform.position, (Vector2)currentNode.up.GetComponent<Transform>().position) <= min)
                {
                    min = Vector2.Distance((Vector2)ghostStart.transform.position, (Vector2)currentNode.up.GetComponent<Transform>().position);
                    temp = currentNode.up;
                }
            }
            if (currentNode.down)
            {
                if (Vector2.Distance((Vector2)ghostStart.transform.position, (Vector2)currentNode.down.GetComponent<Transform>().position) <= min)
                {
                    temp = currentNode.down;
                }
            }

            if (temp == currentNode)
            {
                while (temp != currentNode)
                {
                    int num = Random.Range(0, 4);
                    if (num == 0 && currentNode.up)
                    {
                        temp = currentNode.up;
                    }
                    else if (num == 1 && currentNode.down)
                    {
                        temp = currentNode.down;
                    }
                    else if (num == 2 && currentNode.left)
                    {
                        temp = currentNode.left;
                    }
                    else if (num == 3 && currentNode.right)
                    {
                        temp = currentNode.right;
                    }
                }

            }

            if (temp == currentNode.right)
            {
                print("right");
                GetComponent<SpriteRenderer>().sprite = rightEye;
            } else if (temp == currentNode.left)
            {
                print("left");
                GetComponent<SpriteRenderer>().sprite = leftEye;
            } else if (temp == currentNode.down)
            {
                print("down");
                GetComponent<SpriteRenderer>().sprite = downEye;
            } else
            {
                print("up");
                GetComponent<SpriteRenderer>().sprite = upEye;
            }

            destNode = temp;
        }

        if (Vector2.Distance((Vector2)currentNode.transform.position, (Vector2)ghostStart.transform.position) <= .45f)
        {
            GetComponent<Transform>().position = new Vector2(-.12f, .36f);
            GetComponent<SpriteRenderer>().sprite = defaultSprite;
            isGoingBackToHauntedHouse = false;
            GetComponent<Animator>().enabled = true;
            destNode = ghostStart;
            currentNode = null;
            GetComponent<Animator>().SetTrigger("isBackToNormal");
            GetComponent<Animator>().SetBool("isScatter", false);
            GetComponent<Animator>().ResetTrigger("isScatterAgain");
            timeUntilScatterEnds = 7f;
            StartCoroutine(FreezeGhost());
            return;
        }
    }

    IEnumerator FreezeGhost()
    {
        
        hasBeenReleased = false;
        hasStartedScatter = false;
        isFrozen = true;
        yield return new WaitForSeconds(2f);
        isFrozen = false;
        GetComponent<Animator>().ResetTrigger("isBackToNormal");
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
