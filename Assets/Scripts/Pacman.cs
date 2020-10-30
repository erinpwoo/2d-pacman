using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pacman : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    public int score;
    public Text scoreText;

    public Node currentNode;
    public Node destNode;

    public GameObject[] ghosts;

    public bool justDied;

    public bool isFrozen;

    public GameObject gameManager;

    public Vector2 initPos;

    public Node startNode;

    public int ghostsCaught;

    public Sprite twoHundredPts;
    public Sprite fourHundredPts;
    public Sprite eightHundredPts;
    public Sprite sixteenHundredPts;

    public Node leftEnd;
    public Node rightEnd;
    private Vector2 nextDir;
    private Vector2 oppositeDir;
    private Vector2 currDir;
    private bool areAllGhostsNormal;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.position = new Vector2(0, -2);
        ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        justDied = false;
        isFrozen = true;
        currDir = Vector2.zero;
        oppositeDir = Vector2.zero;
        ghostsCaught = 0;
        destNode = null;
        areAllGhostsNormal = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!justDied && !isFrozen)
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.I))
            {
                nextDir = Vector2.up;
                if (Vector2.up == oppositeDir)
                {
                    ReverseDir(Vector2.up);
                    transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
                    transform.localRotation = Quaternion.Euler(0, 0, 90);
                }
                if (!destNode)
                {
                    destNode = currentNode.up;
                    if (destNode)
                    {
                        lockDirection(Vector2.up);
                        transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
                        transform.localRotation = Quaternion.Euler(0, 0, 90);
                    }
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.M))
            {

                nextDir = Vector2.down;
                if (Vector2.down == oppositeDir)
                {
                    ReverseDir(Vector2.down);
                    transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
                    transform.localRotation = Quaternion.Euler(0, 0, 270);
                }
                if (!destNode)
                {
                    destNode = currentNode.down;
                    if (destNode)
                    {
                        lockDirection(Vector2.down);
                        transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
                        transform.localRotation = Quaternion.Euler(0, 0, 270);
                    }
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.J))
            {
                nextDir = Vector2.left;
                if (Vector2.left == oppositeDir)
                {
                    ReverseDir(Vector2.left);
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                if (!destNode)
                {
                    destNode = currentNode.left;
                    if (destNode)
                    {
                        lockDirection(Vector2.left);
                        transform.localScale = new Vector3(-2.9f, 2.9f, 2.9f);
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.K))
            {
                nextDir = Vector2.right;
                if (Vector2.right == oppositeDir)
                {
                    ReverseDir(Vector2.right);
                    transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                if (!destNode)
                {
                    destNode = currentNode.right;
                    if (destNode)
                    {
                        lockDirection(Vector2.right);
                        transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }
                }
            }
            if (score < 100)
            {
                scoreText.text = score.ToString("d2");
            }
            else
            {
                scoreText.text = score.ToString();
            }
            Move();
        }
        areAllGhostsNormal = true;
        for (int i = 0; i < ghosts.Length; i++)
        {
            if (ghosts[i].GetComponent<Animator>().GetBool("isScatter") || ghosts[i].GetComponent<Animator>().GetBool("isScatterAgain"))
            {
                areAllGhostsNormal = false;
            }
        }
        if (areAllGhostsNormal)
        {
            ghostsCaught = 0;
        }
    }

    void lockDirection(Vector2 dir)
    {
        currDir = dir;
        oppositeDir = dir * (-1);
    }

    void MoveToOtherSide(Node node)
    {
        if (node == leftEnd)
        {
            transform.position = rightEnd.transform.position;
            currentNode = rightEnd;
            destNode = rightEnd.left;
            currDir = Vector2.left;
            oppositeDir = Vector2.right;
            transform.localScale = new Vector3(-2.9f, 2.9f, 2.9f);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        } else
        {
            transform.position = leftEnd.transform.position;
            currentNode = leftEnd;
            destNode = leftEnd.right;
            currDir = Vector2.right;
            oppositeDir = Vector2.left;
            transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void ReverseDir(Vector2 dir)
    {
        Node temp = destNode;
        destNode = currentNode;
        currentNode = temp;
        oppositeDir = dir;
        currDir = dir;
    }

    bool hasGoneOverDestNode()
    {
        float distFromTarget = Vector2.Distance(destNode.transform.position, (Vector2)currentNode.transform.position);
        float distFromCurrNode = Vector2.Distance(transform.position, (Vector2)currentNode.transform.position);
        return distFromCurrNode >= distFromTarget;
    }

    void Move(/*Node dest*/)
    {
        //transform.position = Vector2.MoveTowards(transform.position, dest.transform.position, speed * Time.deltaTime);
        //if (Vector2.Distance((Vector2)dest.transform.position, (Vector2)transform.position) <= .25f)
        //{
        //    currentNode = dest;
        //}

        if ((destNode != currentNode) && destNode)
        {
            if (hasGoneOverDestNode())
            {

                currentNode = destNode;
                transform.position = currentNode.transform.position;
                if (currentNode == leftEnd || currentNode == rightEnd)
                {
                    MoveToOtherSide(currentNode);
                } else
                {
                    Vector2 temp = nextDir;
                    Node nextNode = null;
                    if (temp == Vector2.up && currentNode.up)
                    {
                        nextNode = currentNode.up;
                    } else if (temp == Vector2.down && currentNode.down)
                    {
                        nextNode = currentNode.down;
                    }
                    else if (temp == Vector2.left && currentNode.left)
                    {
                        nextNode = currentNode.left;
                    } else if (temp == Vector2.right && currentNode.right)
                    {
                        nextNode = currentNode.right;
                    }


                    if (!nextNode)
                    {
                        temp = currDir;
                        if (temp == Vector2.up && currentNode.up)
                        {
                            nextNode = currentNode.up;
                        }
                        else if (temp == Vector2.down && currentNode.down)
                        {
                            nextNode = currentNode.down;
                        }
                        else if (temp == Vector2.left && currentNode.left)
                        {
                            nextNode = currentNode.left;
                        }
                        else if (temp == Vector2.right && currentNode.right)
                        {
                            nextNode = currentNode.right;
                        }
                        oppositeDir = currDir * (-1);

                        if (!nextNode)
                        {
                            currDir = Vector2.zero;
                            nextDir = Vector2.zero;
                            oppositeDir = Vector2.zero;
                            destNode = null;
                        } else
                        {
                            destNode = nextNode;
                        }
                        
                    } else
                    {
                        currDir = nextDir;
                        oppositeDir = currDir * (-1);
                        destNode = nextNode;
                    }
                    if (temp != null)
                    {
                        if (temp == Vector2.up)
                        {
                            transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
                            transform.localRotation = Quaternion.Euler(0, 0, 90);
                        }
                        else if (temp == Vector2.down)
                        {
                            transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
                            transform.localRotation = Quaternion.Euler(0, 0, 270);
                        }
                        else if (temp == Vector2.left)
                        {
                            transform.localScale = new Vector3(-2.9f, 2.9f, 2.9f);
                            transform.localRotation = Quaternion.Euler(0, 0, 0);
                        }
                        else if (temp == Vector2.right)
                        {
                            transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
                            transform.localRotation = Quaternion.Euler(0, 0, 0);
                        }
                    }
                }
            }
        }
        transform.position += (Vector3)(currDir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ghost"))
        {
            if (collision.GetComponent<Animator>().GetBool("isScatter") || collision.GetComponent<Animator>().GetBool("isScatterAgain"))
            {
                // send ghost back to haunted house
                ghostsCaught++;
                StartCoroutine(GhostCollisionPoints(collision));

            } else
            {
                justDied = true;
                for (int i = 0; i < ghosts.Length; i++)
                {
                    ghosts[i].GetComponent<Ghost>().pacmanDied = true;
                }
                currentNode = startNode;
                destNode = startNode;
                GetComponent<Transform>().localScale = new Vector3(1.5f, 1.5f, 0);
                GetComponent<Animator>().SetTrigger("killPacman");
                Invoke("Restart", 3);
            }
        }
    }

    IEnumerator GhostCollisionPoints(Collider2D collision)
    {
        collision.GetComponent<Ghost>().isFrozen = true;
        collision.GetComponent<Collider2D>().enabled = false;
        collision.GetComponent<Animator>().enabled = false;
        if (ghostsCaught == 1)
        {
            collision.GetComponent<SpriteRenderer>().sprite = twoHundredPts;
            score += 200;
        } else if (ghostsCaught == 2)
        {
            collision.GetComponent<SpriteRenderer>().sprite = fourHundredPts;
            score += 400;
        } else if (ghostsCaught == 3)
        {
            collision.GetComponent<SpriteRenderer>().sprite = eightHundredPts;
            score += 800;
        } else
        {
            collision.GetComponent<SpriteRenderer>().sprite = sixteenHundredPts;
            score += 1600;
        }
        yield return new WaitForSeconds(2f);
        collision.GetComponent<Ghost>().isGoingBackToHauntedHouse = true;
        collision.GetComponent<Ghost>().isFrozen = false;
    }

    private void Restart()
    {
        gameManager.GetComponent<GameManager>().lives--;
        ghostsCaught = 0;
        if (gameManager.GetComponent<GameManager>().lives <= 0)
        {
            gameManager.GetComponent<GameManager>().GameOver();
        } else
        {
            gameManager.GetComponent<GameManager>().RestartLevel();
        }
        
    }
}
