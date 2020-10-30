﻿using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.position = new Vector2(0, -2);
        ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        justDied = false;
        isFrozen = true;
        ghostsCaught = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!justDied && !isFrozen)
        {
            Move(destNode);
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.I)) && currentNode.up)
            {
                destNode = currentNode.up;
                transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
                transform.localRotation = Quaternion.Euler(0, 0, 90);
            }
            else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.M)) && currentNode.down)
            {
                destNode = currentNode.down;
                transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
                transform.localRotation = Quaternion.Euler(0, 0, 270);
            }
            else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.J)) && currentNode.left)
            {
                destNode = currentNode.left;
                transform.localScale = new Vector3(-2.9f, 2.9f, 2.9f);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.K)) && currentNode.right)
            {
                destNode = currentNode.right;
                transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (score < 100)
            {
                scoreText.text = score.ToString("d2");
            }
            else
            {
                scoreText.text = score.ToString();
            }

            Vector2 curr = transform.position;
            if (curr.x < -3.5f)
            {
                curr.x = 3.25f;
                transform.position = curr;
            }
            if (curr.x > 3.5f)
            {
                curr.x = -3.25f;
                transform.position = curr;
            }
        }

    }

    void Move(Node dest)
    {
        transform.position = Vector2.MoveTowards(transform.position, dest.transform.position, speed * Time.deltaTime);
        if (Vector2.Distance((Vector2)dest.transform.position, (Vector2)transform.position) <= .25f)
        {
            currentNode = dest;
        }
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
        collision.GetComponent<Animator>().enabled = true;
        collision.GetComponent<SpriteRenderer>().sprite = collision.GetComponent<Ghost>().defaultSprite;
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
