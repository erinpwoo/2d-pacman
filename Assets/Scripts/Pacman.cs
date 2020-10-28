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

    private bool justDied;

    public int lives;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.position = new Vector2(0, -2);
        ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        justDied = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!justDied)
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
            print(collision.tag);
            if (collision.GetComponent<Animator>().GetBool("isScatter"))
            {
                // send ghost back to haunted house
            } else
            {
                justDied = true;
                for (int i = 0; i < ghosts.Length; i++)
                {
                    ghosts[i].GetComponent<Ghost>().pacmanDied = true;
                }
            }
        }
    }
}
