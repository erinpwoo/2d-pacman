using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pacman : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 direction = Vector2.zero; // Pacman continues moving in direction of last input

    public int score;
    public Text scoreText;

    public Node currentNode;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.position = new Vector2(0, -2);
    }

    // Update is called once per frame
    void Update()
    {
        Move(currentNode);
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.I)) && currentNode.up) {
            direction = currentNode.up;
            transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        } else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.M)) && currentNode.down)
        {
            direction = Vector2.down;
            transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
            transform.localRotation = Quaternion.Euler(0, 0, 270);
        }
        else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.J)) && currentNode.left)
        {
            direction = Vector2.left;
            transform.localScale = new Vector3(-2.9f, 2.9f, 2.9f);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        } else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.K)) && currentNode.right)
        {
            direction = Vector2.right;
            transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (score < 100)
        {
            scoreText.text = score.ToString("d2");
        } else
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

    void Move(Node dest)
    {
        transform.localPosition += (Vector3) (dest.transform.position * speed * Time.deltaTime);
        if (dest.transform.position == transform.localPosition)
        {
            currentNode = dest;
        }
    }
}
